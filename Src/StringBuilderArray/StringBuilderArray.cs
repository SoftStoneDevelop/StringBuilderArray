using System;
using System.Collections;
using System.Collections.Generic;
#if NET6_0_OR_GREATER
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
#endif
using System.Runtime.CompilerServices;

namespace StringBuilderArray
{
    public sealed partial class StringBuilderArray : IEnumerable<char>
    {
        private StringBuilderArray _previous;
        private StringBuilderArray _next;
        private string[] _buffer;
        private int _size;

        internal const int MaxChunkSize = 8000;

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

        private int InstatnceLength
        {
            get
            {
                var result = 0;
                for (int i = 0; i < _size; i++)
                {
                    result += _buffer[i].Length;
                }

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
            if(_next != null && _next._size == 0)
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
                var tempPrev = new StringBuilderArray(_buffer, _size);
                int newBufferLength = Math.Max(10, Math.Min(_buffer.Length * 2, MaxChunkSize));
                _buffer = new string[newBufferLength];
                _size = 0;

                tempPrev._previous = _previous;
                _previous = tempPrev;

                tempPrev._next = this;
                if (tempPrev._previous != null)
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
            if (tempPr._previous != null)
            {
                tempPr._previous._next = tempPr;
            }
            head._previous = tempPr;

            return this;
        }

        public override string ToString()
        {
#if NET5_0_OR_GREATER
            var newStr = string.Create(
                Length,
                this,
                (dest, state) =>
                {
                    int offset = dest.Length;
                    var current = state;
                    do
                    {
                        var buffer = current._buffer.AsSpan(0, current._size);
                        for (int i = buffer.Length - 1; i >= 0; i--)
                        {
                            string source = buffer[i];
                            offset -= source.Length;
                            source.AsSpan().CopyTo(dest.Slice(offset, source.Length));
                        }

                        current = current._previous;
                    } while (current != null);
                });

            return newStr;
#else
            var newStr = StringHelper.FastAllocateString(Length);
            int offset = newStr.Length;
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
                            var remainingBytes = (newStr.Length - offset) * sizeof(char);
                            var bytesToCopy = source.Length * sizeof(char);
                            fixed (char* pSource = source)
                            {
                                Buffer.MemoryCopy(pSource, pDest + offset, remainingBytes, bytesToCopy);
                            }
                        }

                        current = current._previous;
                    } while (current != null);
                }
            }

            return newStr;
#endif
        }

#if NET5_0_OR_GREATER
        public void ToString(Span<char> resultBuffer)
        {
            var length = Length;
            if (resultBuffer.Length < length)
            {
                throw new ArgumentException($"{nameof(resultBuffer)} length less than {nameof(Length)}");
            }

            int offset = length;
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
                            var remainingBytes = (resultBuffer.Length - offset) * sizeof(char);
                            var bytesToCopy = source.Length * sizeof(char);
                            fixed (char* pSource = source)
                            {
                                Buffer.MemoryCopy(pSource, resultPtr + offset, remainingBytes, bytesToCopy);
                            }
                        }

                        current = current._previous;
                    } while (current != null);
                }
            }
        }
#endif

        public enum IndexDirection
        {
            Unknown = 0,
            FromEnd = 1,
            FromStart = 2
        }

        /// <summary>
        /// Insert a string at the specified index.
        /// </summary>
        public StringBuilderArray InsertByIndex(int charIndex, string value, IndexDirection direction)
        {
            charIndex =
                direction == IndexDirection.FromStart ? Length - charIndex
                : charIndex;

            var currentCharIndex = 0;
            var current = this;
            do
            {
                if (charIndex <= currentCharIndex + current.InstatnceLength)
                {
                    // found a bucket
                    break;
                }

                currentCharIndex += current.InstatnceLength;
                current = current._previous;
            } while (current != null);

            if (current == null)
            {
                throw new ArgumentOutOfRangeException(nameof(charIndex));
            }

            int charIndexInCurrent = charIndex - currentCharIndex;
            InsertByIndex(current, charIndexInCurrent, value);
            return this;
        }

        private void InsertByIndex(
            StringBuilderArray current,
            int charIndex,
            string value)
        {
            var instanceLength = current.InstatnceLength;
            if (instanceLength == 0 || charIndex == 0)
            {
                current.Append(value);
                return;
            }

            var insertIndex = -1;
            int charIndexInInserted = 0;
            var currentCharIndex = 0;
            for (int i = current._size - 1; i >= 0; i--)
            {
                var currentBuffer = current._buffer[i];
                if (charIndex <= currentCharIndex + currentBuffer.Length)
                {
                    insertIndex = i;
                    charIndexInInserted = charIndex - currentCharIndex;
                    break;
                }

                currentCharIndex += currentBuffer.Length;
            }

            if (insertIndex == -1)
            {
                throw new ArgumentOutOfRangeException(nameof(charIndex));
            }

            var str = current._buffer[insertIndex];
            string before = charIndexInInserted == str.Length ? null : str.Substring(0, str.Length - charIndexInInserted);
            string after = charIndexInInserted == 0 ? null : str.Substring(str.Length - charIndexInInserted);

            bool beforeExists = !string.IsNullOrEmpty(before);
            bool afterExists = !string.IsNullOrEmpty(after);

            var insertQueue = new Queue<string>();
            if (beforeExists) { insertQueue.Enqueue(before); }
            insertQueue.Enqueue(value);
            if (afterExists) { insertQueue.Enqueue(after); }

            for (int i = insertIndex + 1; i < current._size; i++)
            {
                insertQueue.Enqueue(current._buffer[i]);
            }

            current._size = insertIndex;
            while (insertQueue.Count > 0 && current._size < current._buffer.Length)
            {
                current._buffer[current._size++] = insertQueue.Dequeue();
            }

            while (insertQueue.Count > 0)
            {
                current = current.Append(insertQueue.Dequeue());
            }
        }

        #region IEnumerable

        public IEnumerator<char> GetEnumerator()
        {
            return new StringBuilderArrayEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StringBuilderArrayEnumerator(this);
        }

        #endregion

#if NET6_0_OR_GREATER

        public StringBuilderArray Append([InterpolatedStringHandlerArgument("")] ref StringBuilderArrayStringHandler handler) => this;

        public StringBuilderArray Append(IFormatProvider provider, [InterpolatedStringHandlerArgument("", "provider")] ref StringBuilderArrayStringHandler handler) => this;

        public StringBuilderArray AppendLine([InterpolatedStringHandlerArgument("")] ref StringBuilderArrayStringHandler handler) => AppendLine();

        public StringBuilderArray AppendLine(IFormatProvider provider, [InterpolatedStringHandlerArgument("", "provider")] ref StringBuilderArrayStringHandler handler) => AppendLine();

        public StringBuilderArray InsertByIndex(int charIndex, IndexDirection directionIndex, [InterpolatedStringHandlerArgument("", "charIndex", "directionIndex")] ref StringBuilderArrayInsertStringHandler handler) => this;

        public StringBuilderArray InsertByIndex(IFormatProvider provider, int charIndex, IndexDirection directionIndex, [InterpolatedStringHandlerArgument("", "provider", "charIndex", "directionIndex")] ref StringBuilderArrayInsertStringHandler handler) => this;

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
            private readonly int _charIndex;
            private readonly IndexDirection _directionIndex;

            public StringBuilderArrayInsertStringHandler(
                int literalLength,
                int formattedCount,
                StringBuilderArray stringBuilder,
                int charIndex,
                IndexDirection directionIndex)
            {
                _stringBuilder = stringBuilder;
                _provider = null;
                _hasCustomFormatter = false;
                _charIndex = charIndex;
                _directionIndex = directionIndex;
            }

            public StringBuilderArrayInsertStringHandler(
                int literalLength,
                int formattedCount,
                StringBuilderArray stringBuilder,
                IFormatProvider provider,
                int charIndex,
                IndexDirection directionIndex)
            {
                _stringBuilder = stringBuilder;
                _provider = provider;
                _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
                _charIndex = charIndex;
                _directionIndex = directionIndex;
            }

            public void AppendLiteral(string value) => _stringBuilder.InsertByIndex(_charIndex, value, _directionIndex);

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
                    _stringBuilder.InsertByIndex(_charIndex, ((IFormattable)value).ToString(format: null, _provider), _directionIndex);
                }
                else if (value is not null)
                {
                    _stringBuilder.InsertByIndex(_charIndex, value.ToString(), _directionIndex);
                }
            }

            public void AppendFormatted<T>(T value, string format)
            {
                if (_hasCustomFormatter)
                {
                    AppendCustomFormatter(value, format);
                }
                else if (value is IFormattable formattable)
                {
                    _stringBuilder.InsertByIndex(_charIndex, formattable.ToString(format, _provider), _directionIndex);
                }
                else if (value is not null)
                {
                    _stringBuilder.InsertByIndex(_charIndex, value.ToString(), _directionIndex);
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
                            _stringBuilder.InsertByIndex(_charIndex, _whitespace, _directionIndex);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < alignment; i++)
                    {
                        _stringBuilder.InsertByIndex(_charIndex, _whitespace, _directionIndex);
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
                    _stringBuilder.InsertByIndex(_charIndex, value, _directionIndex);
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
                    _stringBuilder.InsertByIndex(_charIndex, formatter.Format(format, value, _provider), _directionIndex);
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