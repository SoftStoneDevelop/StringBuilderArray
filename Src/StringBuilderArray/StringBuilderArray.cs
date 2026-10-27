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
        private int _instatnceLength;

        internal const int MaxChunkSize = 4000;

        public StringBuilderArray(int bufferSize = 10)
        {
            _buffer = new string[bufferSize];
        }

        public StringBuilderArray(string[] buffer, int usedSize, int instatnceLength)
        {
            _buffer = buffer;
            _size = usedSize;
            _instatnceLength = instatnceLength;
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
                    result += current._instatnceLength;
                    current = current._previous;
                } while (current != null);

                return result;
            }
        }

        public StringBuilderArray Append(string str)
        {
            if (_size == _buffer.Length)
            {
                MakePlace();
            }

            _buffer[_size++] = str;
            _instatnceLength += str.Length;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MakePlace()
        {
            if(_next != null)
            {
                var tempBuffer = _buffer;
                _buffer = _next._buffer;
                _next._buffer = tempBuffer;

                var tempSize = _size;
                _size = _next._size;
                _next._size = tempSize;

                var tempInstatnceLength = _instatnceLength;
                _instatnceLength = _next._instatnceLength;
                _next._instatnceLength = tempInstatnceLength;

                _next._previous = _previous;
                if (_previous != null)
                {
                    _previous._next = _next;
                }
                _previous = _next;

                var tempNext = _next._next;
                _next._next = this;
                _next = tempNext;
            }
            else
            {
                var tempPrev = new StringBuilderArray(_buffer, _size, _instatnceLength);
                _size = 0;
                _instatnceLength = 0;
                _buffer = new string[Math.Max(10, Math.Min(_buffer.Length * 2, MaxChunkSize))];

                tempPrev._previous = _previous;
                _previous = tempPrev;

                tempPrev._next = this;
                if (tempPrev._previous != null)
                {
                    tempPrev._previous._next = tempPrev;
                }
            }
        }

        private static void AppendInternal(StringBuilderArray builder, string str)
        {
            if (builder._size == builder._buffer.Length)
            {
                if (builder._next != null && builder._next._size < builder._next._buffer.Length)
                {
                    if (builder._next._size > 0)
                    {
                        ShiftRight(builder._next._buffer);
                    }

                    builder._next._buffer[0] = str;
                    builder._next._instatnceLength += str.Length;
                    builder._next._size++;
                }
                else
                {
                    var tempPrev = new StringBuilderArray(builder._buffer, builder._size, builder._instatnceLength);
                    builder._size = 0;
                    builder._instatnceLength = 0;
                    builder._buffer = new string[Math.Max(10, Math.Min(builder._buffer.Length * 2, MaxChunkSize))];

                    tempPrev._previous = builder._previous;
                    builder._previous = tempPrev;
                    tempPrev._next = builder;
                    if (tempPrev._previous != null)
                    {
                        tempPrev._previous._next = tempPrev;
                    }
                    builder._buffer[builder._size++] = str;
                    builder._instatnceLength += str.Length;
                }
            }
            else
            {
                builder._buffer[builder._size++] = str;
                builder._instatnceLength += str.Length;
            }
        }

#if NET6_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ShiftRight(Span<string> span)
        {
            for (var i = span.Length - 1; i > 0; i--)
            {
                span[i] = span[i - 1];
            }
        }
#else
        private static void ShiftRight(string[] buffer)
        {
            for (var i = buffer.Length - 1; i > 0; i--)
            {
                buffer[i] = buffer[i - 1];
            }
        }  
#endif

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
            _size = 0;
            _instatnceLength = 0;
#if NET6_0_OR_GREATER
            Array.Clear(_buffer);
#else
            Array.Clear(_buffer, 0, _buffer.Length);
#endif
            if (_previous == null && _next == null)
            {
                return this;
            }

            var tailPrevious = _previous;
            var currentPrevious = _previous;
            var currentNext = _next;
            _previous = null;
            if (tailPrevious != null)
            {
                tailPrevious._next = _next;
            }

            while (currentPrevious != null)
            {
                currentPrevious._size = 0;
                currentPrevious._instatnceLength = 0;
#if NET6_0_OR_GREATER
                Array.Clear(currentPrevious._buffer);
#else
                Array.Clear(currentPrevious._buffer, 0, currentPrevious._buffer.Length);
#endif
                if (currentPrevious._previous == null)
                {
                    currentPrevious._previous = this;
                    _next = currentPrevious;
                    break;
                }
                else
                {
                    currentPrevious = currentPrevious._previous;
                }
            }

            if (currentNext != null)
            {
                if (tailPrevious != null)
                {
                    currentNext._previous = tailPrevious;
                }
                else
                {
                    currentNext._previous = this;
                }
            }
            
            while (currentNext != null)
            {
                currentNext._size = 0;
                currentNext._instatnceLength = 0;
#if NET6_0_OR_GREATER
                Array.Clear(currentNext._buffer);
#else
                Array.Clear(currentNext._buffer, 0, currentNext._buffer.Length);
#endif
                if (currentNext._next == null)
                {
                    break;
                }
                else
                {
                    currentNext = currentNext._next;
                }
            }

            return this;
        }

        public override string ToString()
        {
#if NET6_0_OR_GREATER
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
                        for (int i = current._size - 1; i >= 0; i--)
                        {
                            var sourceSpan = buffer[i].AsSpan();
                            offset -= sourceSpan.Length;
                            sourceSpan.CopyTo(dest.Slice(offset, sourceSpan.Length));
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
                        var buffer = current._buffer;
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
                            var source = buffer[i].AsSpan();
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

        /// <summary>
        /// Insert a string at the specified index.
        /// </summary>
        public void InsertByIndex(int charIndex, string value, IndexDirection direction)
        {
            charIndex =
                direction == IndexDirection.FromStart ? Length - charIndex
                : charIndex;

            var currentCharIndex = 0;
            var current = this;
            do
            {
                if (charIndex <= currentCharIndex + current._instatnceLength)
                {
                    // found a bucket
                    break;
                }

                currentCharIndex += current._instatnceLength;
                current = current._previous;
            } while (current != null);

            if (current == null)
            {
                throw new ArgumentOutOfRangeException(nameof(charIndex));
            }

            int charIndexInCurrent = charIndex - currentCharIndex;
            InsertByIndex(current, charIndexInCurrent, value);
        }

        private void InsertByIndex(
            StringBuilderArray current,
            int charIndex,
            string value)
        {
            if (current._instatnceLength == 0 || charIndex == 0)
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
            current._instatnceLength -= str.Length;
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
                current._instatnceLength -= current._buffer[i].Length;
            }

            current._size = insertIndex;
            while (insertQueue.Count > 0 && current._size < current._buffer.Length)
            {
                var insertedString = insertQueue.Dequeue();
                current._buffer[current._size++] = insertedString;
                current._instatnceLength += insertedString.Length;
            }

            while (insertQueue.Count > 0)
            {
                AppendInternal(current, insertQueue.Dequeue());
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
                else if (value is IFormattable formattable)
                {
                    _stringBuilder.Append(formattable.ToString(format: null, _provider));
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
                else if (value is IFormattable formattable)
                {
                    _stringBuilder.Append(formattable.ToString(format, _provider));
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
                else if (value is IFormattable formattable)
                {
                    _stringBuilder.InsertByIndex(_charIndex, formattable.ToString(format: null, _provider), _directionIndex);
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