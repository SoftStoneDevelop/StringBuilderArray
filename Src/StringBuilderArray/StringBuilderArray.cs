using System;
using System.Runtime.CompilerServices;

namespace StringBuilderArray
{
    public class StringBuilderArray
    {
        private StringBuilderArray _previous;
        private string[] _buffer;
        private int _size;
        private int _length;

        internal static int MaxChunkSize = 8000;

        public StringBuilderArray(int bufferSize = 10)
        {
            _buffer = new string[bufferSize];
        }

        public StringBuilderArray(string[] buffer, int usedSize)
        {
            _buffer = buffer;
            _size = usedSize;
            if (_size > 0)
            {
                for (int i = 0; i < usedSize; i++)
                {
                    _length += buffer[i].Length;
                }
            }
        }

        private StringBuilderArray(StringBuilderArray stringBuilderArray)
        {
            _buffer = stringBuilderArray._buffer;
            _size = stringBuilderArray._size;
            _length = stringBuilderArray._length;
            _previous = stringBuilderArray._previous;
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var result = 0;
                var current = this;
                do
                {
                    result += current._length;
                    current = current._previous;
                } while (current != null);

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringBuilderArray Append(string str)
        {
            if (_size == _buffer.Length)
            {
                Grow();
            }
            
            _buffer[_size++] = str;
            _length += str.Length;
            return this;
        }

        public StringBuilderArray AppendLine(string str)
        {
            Append(str);
            return Append(Environment.NewLine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StringBuilderArray Append(string[] strings)
        {
            foreach (var str in strings)
            {
                Append(str);
            }

            return this;
        }

        public StringBuilderArray AppendLine(string[] strings)
        {
            foreach (var str in strings)
            {
                Append(str);
                Append(Environment.NewLine);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Grow()
        {
            _previous = new StringBuilderArray(this);

            int newBufferLength = Math.Max(10, Math.Min(_buffer.Length * 2, MaxChunkSize));
            _buffer = new string[newBufferLength];
            _size = 0;
            _length = 0;
        }

        public override string ToString()
        {
            var newStr = StringHelper.FastAllocateString(Length);
            int offset = newStr.Length;
            var destBytes = newStr.Length * sizeof(char);
            var current = this;

            unsafe
            {
                fixed (char* pDest = newStr)
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
                                Buffer.MemoryCopy(pSource, pDest + offset, destBytes, source.Length * sizeof(char));
                            }
                        }

                        current = current._previous;
                    } while (current != null);
                }
            }

            return newStr;
        }
    }
}