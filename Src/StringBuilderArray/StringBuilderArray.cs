using System;
using System.Collections;
using System.Collections.Generic;
#if NET6_0_OR_GREATER
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
#endif
using System.Runtime.CompilerServices;

namespace StringBuilderArray
{
    public class StringBuilderArray : IEnumerable<string>
    {
        private StringBuilderArray _previous;
        private StringBuilderArray _next;
        private string[] _buffer;
        private int _size;

        internal static readonly int MaxChunkSize = 8000;

        public StringBuilderArray(int bufferSize = 10)
        {
            _buffer = new string[bufferSize];
        }

        public StringBuilderArray(string[] buffer, int usedSize)
        {
            _buffer = buffer;
            _size = usedSize;
        }

        private StringBuilderArray()
        {
        }

        public int Length
        {
            get
            {
                var result = 0;
                var current = this;
                do
                {
                    for (int i = 0; i < current._size; i++)
                    {
                        result += current._buffer[i].Length;
                    }

                    current = current._previous;
                } while (current != null);

                return result;
            }
        }

        public StringBuilderArray Append(string str)
        {
            if (_size == _buffer.Length)
            {
                MakeThisPrevious();
            }

            _buffer[_size++] = str;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakeThisPrevious()
        {
            if(_next != null)
            {
                /*
                 * Move data(and prev/next links) between this and emptyNext(empty because outlived Clear)
                 * before
                 * (prev)->(this)->(emptyNext)->...
                 * 
                 * after
                 * (prev)->(next)->(emptyNext)->...
                 * 
                 */

                var tempBuffer = _buffer;
                _buffer = _next._buffer;
                _next._buffer = tempBuffer;

                var tempSize = _size;
                _next._size = tempSize;
                _size = 0;

                _previous = _next;
                _next._previous = this;

                var tempNext = _next;
                _next = tempNext._next;
                tempNext._next = this;
            }
            else
            {
                /* Move data(and prev/next links) between this and newChunk
                 * before
                 * (prev)->(this) (newChunk)
                 * 
                 * after
                 * (prev)->(newChunk)->(this)
                 * 
                 */
                var tempPrev = new StringBuilderArray();
                tempPrev._buffer = _buffer;
                int newBufferLength = Math.Max(10, Math.Min(_buffer.Length * 2, MaxChunkSize));
                _buffer = new string[newBufferLength];
                
                tempPrev._size = _size;
                _size = 0;

                tempPrev._previous = _previous;
                _previous = tempPrev;

                tempPrev._next = this;
                if(tempPrev._previous != null)
                {
                    tempPrev._previous._next = tempPrev;
                }
            }
        }

        public StringBuilderArray AppendLine(string str)
        {
            Append(str);
            return Append(Environment.NewLine);
        }

        public StringBuilderArray AppendLine()
        {
            return Append(Environment.NewLine);
        }

        public StringBuilderArray Append(IList<string> strings)
        {
            foreach (var str in strings)
            {
                Append(str);
            }

            return this;
        }

        public StringBuilderArray AppendLine(IList<string> strings)
        {
            foreach (var str in strings)
            {
                Append(str);
                Append(Environment.NewLine);
            }

            return this;
        }

        public StringBuilderArray Clear()
        {
            if(_previous == null && _next == null)
            {
                _size = 0;
#if NET6_0_OR_GREATER
                Array.Clear(_buffer);
#else
                Array.Clear(_buffer, 0, _buffer.Length);
#endif
                return this;
            }

            StringBuilderArray head = null;
            var current = this;
            do
            {
                head = current;
                current._size = 0;
#if NET6_0_OR_GREATER
                Array.Clear(current._buffer);
#else
                Array.Clear(current._buffer, 0, current._buffer.Length);
#endif
                current = current._previous;
            } while (current != null);

            current = this._next;
            while (current != null)
            {
                current._size = 0;
#if NET6_0_OR_GREATER
                Array.Clear(current._buffer);
#else
                Array.Clear(current._buffer, 0, current._buffer.Length);
#endif
                current = current._next;
            }

            /* Move data(and prev/next links) between head and tail
                 * before
                 * (head)->...->(prev)->(this)
                 * 
                 * after
                 * (this)->...->(prev)->(head)
                 * 
                 */
            var tempBuffer = _buffer;
            _buffer = head._buffer;
            head._buffer = tempBuffer;

            var tempPr = _previous;
            _previous = null;
            if(tempPr._previous != null)
            {
                tempPr._previous._next = tempPr;
            }
            head._previous = tempPr;

            return this;
        }

        public override string ToString()
        {
#if NET6_0_OR_GREATER
            var newStr = string.Create<object>(Length, null, (_, _) => {});
#else
            var newStr = StringHelper.FastAllocateString(Length);
#endif
            int offset = newStr.Length;
            var destBytes = newStr.Length * sizeof(char);
            var current = this;

            unsafe
            {
                fixed (char* pDest = newStr)
                {
                    do
                    {
#if NET5_0_OR_GREATER
                        var buffer = current._buffer.AsSpan();
#else
                        var buffer = current._buffer;
#endif
                        for (int i = current._size - 1; i >= 0; i--)
                        {
                            var source = buffer[i];
                            offset -= source.Length;
                            fixed (char* pSource = source)
                            {
                                Buffer.MemoryCopy(pSource, pDest + offset, destBytes, source.Length * sizeof(char));
                            }
                        }

                        current = current._previous;
                    } while (current != null);
                }
            }

            return newStr;
        }

#if NET5_0_OR_GREATER
        public void ToString(Span<char> resultBuffer)
        {
            var length = Length;
            if (length < resultBuffer.Length)
            {
                throw new ArgumentException($"{nameof(resultBuffer)} length less than {nameof(Length)}");
            }

            int offset = length;
            var destBytes = length * sizeof(char);
            var current = this;
            
            unsafe
            {
                fixed(char* resultPtr = resultBuffer)
                {
                    do
                    {
                        var buffer = current._buffer.AsSpan();
                        for (int i = current._size - 1; i >= 0; i--)
                        {
                            var source = buffer[i];
                            offset -= source.Length;
                            fixed (char* pSource = source)
                            {
                                Buffer.MemoryCopy(pSource, resultPtr + offset, destBytes, source.Length * sizeof(char));
                            }
                        }

                        current = current._previous;
                    } while (current != null);
                }
            }
        }
#endif
        /// <summary>
        /// Search string in builder(not full-text search, compares only parts of which were filled)
        /// </summary>
        /// <returns>return index fron the end; -1 if not found</returns>
        public int GetIndex(string searchStr)
        {
            var index = 0;
            var current = this;
            do
            {
#if NET5_0_OR_GREATER
                var buffer = current._buffer.AsSpan();
#else
                var buffer = current._buffer;
#endif
                for (int i = current._size - 1; i >= 0; i--)
                {
#if NET6_0_OR_GREATER
                    if (IsTheSameSequence(current, i, searchStr))
                    {
                        return index;
                    }
#else
                    if (buffer[i] == searchStr)
                    {
                        return index;
                    }
#endif

                    index++;
                }

                current = current._previous;
            } while (current != null);

            return -1;
        }

#if NET6_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsTheSameSequence(StringBuilderArray current, int bufferIndex, ReadOnlySpan<char> searchStr)
        {
            var indexInsearchStr = searchStr.Length - 1;
            do
            {
                var buffer = current._buffer.AsSpan();
                if (bufferIndex == -1)
                {
                    bufferIndex = current._size - 1;
                }

                for (; bufferIndex >= 0; bufferIndex--)
                {
                    var valSpan = buffer[bufferIndex].AsSpan();
                    for (int i = valSpan.Length - 1; i >= 0; i--)
                    {
                        if (valSpan[i] != searchStr[indexInsearchStr--])
                        {
                            return false;
                        }

                        if (indexInsearchStr == -1 && i == 0)
                        {
                            return true;
                        }
                    }
                }

                bufferIndex = -1;
                current = current._next;
            } while (current != null);

            return false;
        }
#endif

        public bool Insert(int indexFromTheEnd, string value)
        {
            var index = 0;
            var current = this;
            do
            {
                if (indexFromTheEnd <= index + current._size - 1)
                {
                    break;
                }

                index += current._size;
                current = current._previous;
            } while (current != null);

            if (current != null)
            {
                var buffer = current._buffer;
                int shiftIndex = current._size - (indexFromTheEnd - index) - 1;
                Shift(current, shiftIndex);

                buffer[shiftIndex] = value;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Shift(StringBuilderArray current, int index)
        {
            string movedValue = null;
#if NET5_0_OR_GREATER
            Span<string> buffer;
#else
            string[] buffer;
#endif

            do
            {
                if(current == null)
                {
                    MakeThisPrevious();
                    Append(movedValue);
                    movedValue = null;
                    break;
                }

#if NET5_0_OR_GREATER
                buffer = current._buffer.AsSpan();
#else
                buffer = current._buffer;
#endif

                if (buffer.Length == current._size)
                {
                    movedValue = buffer[buffer.Length - 1];
#if NET5_0_OR_GREATER
                    ShiftRight(buffer);
#else
                    ShiftRight();
#endif
                    index = 0;
                }
                else
                {
#if NET5_0_OR_GREATER
                    ShiftRight(buffer);
#else
                    ShiftRight();
#endif
                    buffer[index] = movedValue;
                    current._size++;
                    movedValue = null;
                    break;
                }

                current = current._next;
            } while (true);

#if NET5_0_OR_GREATER
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            void ShiftRight(Span<string> span)
            {
                for (var i = span.Length - 1; i > index; i--)
                {
                    span[i] = span[i - 1];
                }
            }
#else
            void ShiftRight()
            {
                for (var i = buffer.Length - 1; i > index; i--)
                {
                    buffer[i] = buffer[i - 1];
                }
            }
#endif
        }

        #region IEnumerable

        public IEnumerator<string> GetEnumerator()
        {
            return new StringBuilderArrayEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StringBuilderArrayEnumerator(this);
        }

        public struct StringBuilderArrayEnumerator : IEnumerator<string>
        {
            private StringBuilderArray _currentChunk;
            private int _currentIndex;
            private string _current;

            public StringBuilderArrayEnumerator(StringBuilderArray stringBuilderArray)
            {
                _currentIndex = 0;
                _current = null;

                var tempChunk = stringBuilderArray;
                do
                {
                    _currentChunk = tempChunk;
                    tempChunk = _currentChunk._previous;
                } while (tempChunk != null);
            }

#if NET5_0_OR_GREATER
            readonly public string Current => _current;

            readonly object IEnumerator.Current => _current;
#else
            public string Current => _current;

            object IEnumerator.Current => _current;
#endif

            public void Dispose()
            {
                _currentChunk = null;
                _current = null;
            }

            public bool MoveNext()
            {
                if(_currentChunk._size == 0)
                {
                    return false;
                }

                if (_currentChunk._size == _currentIndex)
                {
                    _currentIndex = 0;
                    _currentChunk = _currentChunk._next;
                }

                if (_currentChunk == null || _currentChunk._size == 0)
                {
                    return false;
                }

                if (_currentChunk._size == 0)
                {
                    return false;
                }

                _current = _currentChunk._buffer[_currentIndex++];
                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        #endregion

#if NET6_0_OR_GREATER

        public StringBuilderArray Append([InterpolatedStringHandlerArgument("")] ref StringBuilderArrayStringHandler handler) => this;

        public StringBuilderArray Append(IFormatProvider provider, [InterpolatedStringHandlerArgument("", "provider")] ref StringBuilderArrayStringHandler handler) => this;

        public StringBuilderArray AppendLine([InterpolatedStringHandlerArgument("")] ref StringBuilderArrayStringHandler handler) => AppendLine();

        public StringBuilderArray AppendLine(IFormatProvider provider, [InterpolatedStringHandlerArgument("", "provider")] ref StringBuilderArrayStringHandler handler) => AppendLine();

        public StringBuilderArray Insert(int indexFromTheEnd, [InterpolatedStringHandlerArgument("", "indexFromTheEnd")] ref StringBuilderArrayInsertStringHandler handler) => this;

        public StringBuilderArray Insert(IFormatProvider provider, int indexFromTheEnd, [InterpolatedStringHandlerArgument("", "provider", "indexFromTheEnd")] ref StringBuilderArrayInsertStringHandler handler) => this;

        internal static readonly string _whitespace = " ";

        [EditorBrowsable(EditorBrowsableState.Never)]
        [InterpolatedStringHandler]
        public struct StringBuilderArrayStringHandler
        {
            private readonly StringBuilderArray _stringBuilder;
            private readonly IFormatProvider _provider;
            private readonly bool _hasCustomFormatter;

            public StringBuilderArrayStringHandler(int literalLength, int formattedCount, StringBuilderArray stringBuilder)
            {
                _stringBuilder = stringBuilder;
                _provider = null;
                _hasCustomFormatter = false;
            }

            public StringBuilderArrayStringHandler(int literalLength, int formattedCount, StringBuilderArray stringBuilder, IFormatProvider provider)
            {
                _stringBuilder = stringBuilder;
                _provider = provider;
                _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
            }

            public void AppendLiteral(string value) => _stringBuilder.Append(value);

            #region AppendFormatted

            #region AppendFormatted T
            public void AppendFormatted<T>(T value)
            {
                if (_hasCustomFormatter)
                {
                    AppendCustomFormatter(value, format: null);
                }
                else if (value is IFormattable)
                {
                    _stringBuilder.Append(((IFormattable)value).ToString(format: null, _provider));
                }
                else if (value is not null)
                {
                    _stringBuilder.Append(value.ToString());
                }
            }

            public void AppendFormatted<T>(T value, string format)
            {
                if (_hasCustomFormatter)
                {
                    AppendCustomFormatter(value, format);
                }
                else if (value is IFormattable)
                {
                    _stringBuilder.Append(((IFormattable)value).ToString(format, _provider));
                }
                else if (value is not null)
                {
                    _stringBuilder.Append(value.ToString());
                }
            }

            public void AppendFormatted<T>(T value, int alignment) => AppendFormatted(value, alignment, format: null);

            public void AppendFormatted<T>(T value, int alignment, string format)
            {
                if (alignment == 0)
                {
                    AppendFormatted(value, format);
                }
                else if (alignment < 0)
                {
                    int start = _stringBuilder.Length;
                    AppendFormatted(value, format);
                    int paddingRequired = -alignment - (_stringBuilder.Length - start);
                    if (paddingRequired > 0)
                    {
                        for (int i = 0; i < paddingRequired; i++)
                        {
                            _stringBuilder.Append(_whitespace);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < alignment; i++)
                    {
                        _stringBuilder.Append(_whitespace);
                    }
                    AppendFormatted(value, format);
                }
            }
            #endregion

            #region AppendFormatted string
            public void AppendFormatted(string value)
            {
                if (!_hasCustomFormatter)
                {
                    _stringBuilder.Append(value);
                }
                else
                {
                    AppendFormatted<string>(value);
                }
            }

            public void AppendFormatted(string value, int alignment = 0, string format = null) => AppendFormatted<string>(value, alignment, format);
            #endregion

            #region AppendFormatted object
            public void AppendFormatted(object value, int alignment = 0, string format = null) => AppendFormatted<object>(value, alignment, format);
            #endregion

            #endregion

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void AppendCustomFormatter<T>(T value, string format)
            {
                Debug.Assert(_hasCustomFormatter);
                Debug.Assert(_provider != null);

                ICustomFormatter formatter = (ICustomFormatter)_provider.GetFormat(typeof(ICustomFormatter));
                Debug.Assert(formatter != null, "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

                if (formatter is not null)
                {
                    _stringBuilder.Append(formatter.Format(format, value, _provider));
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static bool HasCustomFormatter(IFormatProvider provider)
            {
                Debug.Assert(provider is not null);
                Debug.Assert(provider is not CultureInfo || provider.GetFormat(typeof(ICustomFormatter)) is null, "Expected CultureInfo to not provide a custom formatter");
                return
                    provider.GetType() != typeof(CultureInfo) &&
                    provider.GetFormat(typeof(ICustomFormatter)) != null;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [InterpolatedStringHandler]
        public struct StringBuilderArrayInsertStringHandler
        {
            private readonly StringBuilderArray _stringBuilder;
            private readonly IFormatProvider _provider;
            private readonly bool _hasCustomFormatter;
            private int _indexFromTheEnd;

            public StringBuilderArrayInsertStringHandler(int literalLength, int formattedCount, StringBuilderArray stringBuilder, int indexFromTheEnd)
            {
                _stringBuilder = stringBuilder;
                _provider = null;
                _hasCustomFormatter = false;
                _indexFromTheEnd = indexFromTheEnd;
            }

            public StringBuilderArrayInsertStringHandler(int literalLength, int formattedCount, StringBuilderArray stringBuilder, IFormatProvider provider, int indexFromTheEnd)
            {
                _stringBuilder = stringBuilder;
                _provider = provider;
                _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
                _indexFromTheEnd = indexFromTheEnd;
            }

            public void AppendLiteral(string value) => _stringBuilder.Insert(_indexFromTheEnd, value);

            #region AppendFormatted

            #region AppendFormatted T
            public void AppendFormatted<T>(T value)
            {
                if (_hasCustomFormatter)
                {
                    AppendCustomFormatter(value, format: null);
                }
                else if (value is IFormattable)
                {
                    _stringBuilder.Insert(_indexFromTheEnd, ((IFormattable)value).ToString(format: null, _provider));
                }
                else if (value is not null)
                {
                    _stringBuilder.Insert(_indexFromTheEnd, value.ToString());
                }
            }

            public void AppendFormatted<T>(T value, string format)
            {
                if (_hasCustomFormatter)
                {
                    AppendCustomFormatter(value, format);
                }
                else if (value is IFormattable)
                {
                    _stringBuilder.Insert(_indexFromTheEnd, ((IFormattable)value).ToString(format, _provider));
                }
                else if (value is not null)
                {
                    _stringBuilder.Insert(_indexFromTheEnd, value.ToString());
                }
            }

            public void AppendFormatted<T>(T value, int alignment) => AppendFormatted(value, alignment, format: null);

            public void AppendFormatted<T>(T value, int alignment, string format)
            {
                if (alignment == 0)
                {
                    AppendFormatted(value, format);
                }
                else if (alignment < 0)
                {
                    int start = _stringBuilder.Length;
                    AppendFormatted(value, format);
                    int paddingRequired = -alignment - (_stringBuilder.Length - start);
                    if (paddingRequired > 0)
                    {
                        for (int i = 0; i < paddingRequired; i++)
                        {
                            _stringBuilder.Insert(_indexFromTheEnd, _whitespace);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < alignment; i++)
                    {
                        _stringBuilder.Insert(_indexFromTheEnd, _whitespace);
                    }
                    AppendFormatted(value, format);
                }
            }
            #endregion

            #region AppendFormatted string
            public void AppendFormatted(string value)
            {
                if (!_hasCustomFormatter)
                {
                    _stringBuilder.Insert(_indexFromTheEnd, value);
                }
                else
                {
                    AppendFormatted<string>(value);
                }
            }

            public void AppendFormatted(string value, int alignment = 0, string format = null) => AppendFormatted<string>(value, alignment, format);
            #endregion

            #region AppendFormatted object
            public void AppendFormatted(object value, int alignment = 0, string format = null) => AppendFormatted<object>(value, alignment, format);
            #endregion

            #endregion

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void AppendCustomFormatter<T>(T value, string format)
            {
                Debug.Assert(_hasCustomFormatter);
                Debug.Assert(_provider != null);

                ICustomFormatter formatter = (ICustomFormatter)_provider.GetFormat(typeof(ICustomFormatter));
                Debug.Assert(formatter != null, "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

                if (formatter is not null)
                {
                    _stringBuilder.Insert(_indexFromTheEnd, formatter.Format(format, value, _provider));
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static bool HasCustomFormatter(IFormatProvider provider)
            {
                Debug.Assert(provider is not null);
                Debug.Assert(provider is not CultureInfo || provider.GetFormat(typeof(ICustomFormatter)) is null, "Expected CultureInfo to not provide a custom formatter");
                return
                    provider.GetType() != typeof(CultureInfo) &&
                    provider.GetFormat(typeof(ICustomFormatter)) != null;
            }
        }
#endif
    }
}