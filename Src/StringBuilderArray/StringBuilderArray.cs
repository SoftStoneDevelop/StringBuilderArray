using System;
using System.Collections;
using System.Collections.Generic;
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
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    if(buffer[i] == searchStr)
                    {
                        return index;
                    }
                    
                    index++;
                }

                current = current._previous;
            } while (current != null);

            return -1;
        }

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
    }
}