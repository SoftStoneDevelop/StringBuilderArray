using System;
using System.Collections;
using System.Collections.Generic;

namespace StringBuilderArray
{
    public sealed partial class StringBuilderArray
    {
        public struct StringBuilderArrayEnumerator : IEnumerator<char>
        {
            private StringBuilderArray _currentChunk;
            private int _currentBufferIndex;
            private int _currentCharIndex;
            private char _current;
            private string _currentString;

            public StringBuilderArrayEnumerator(StringBuilderArray stringBuilderArray)
            {
                _currentBufferIndex = 0;
                _currentCharIndex = 0;
                _current = char.MinValue;
                _currentString = null;

                var tempChunk = stringBuilderArray;
                do
                {
                    _currentChunk = tempChunk;
                    tempChunk = _currentChunk._previous;
                } while (tempChunk != null);

                if (_currentChunk._size > 0 && _currentChunk._buffer[0].Length > 0)
                {
                    _currentString = _currentChunk._buffer[0];
                    _current = _currentString[0];
                }
            }

#if NET5_0_OR_GREATER
            readonly public char Current => _current;

            readonly object IEnumerator.Current => _current;
#else
            public char Current => _current;

            object IEnumerator.Current => _current;
#endif

            public void Dispose()
            {
                _currentChunk = null;
                _currentString = null;
            }

            public bool MoveNext()
            {
                if (_currentString == null || _currentString.Length == 0)
                {
                    return false;
                }

                if (_currentString.Length == _currentCharIndex)
                {
                    if (_currentChunk._size == _currentBufferIndex + 1)
                    {
                        _currentBufferIndex = 0;
                        _currentCharIndex = 0;
                        _currentChunk = _currentChunk._next;
                        if (_currentChunk == null || _currentChunk._size == 0)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        _currentBufferIndex++;
                        _currentCharIndex = 0;
                    }

                    _currentString = _currentChunk._buffer[_currentBufferIndex];
                }

                _current = _currentString[_currentCharIndex++];
                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
