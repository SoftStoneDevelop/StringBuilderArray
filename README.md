<h1 align="center">
  <a>StringBuilderArray</a>
</h1>

<h3 align="center">

  [![Nuget](https://img.shields.io/nuget/v/StringBuilderArray?logo=StringBuilderArray)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Downloads](https://img.shields.io/nuget/dt/StringBuilderArray.svg)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Stars](https://img.shields.io/github/stars/SoftStoneDevelop/StringBuilderArray?color=brightgreen)](https://github.com/SoftStoneDevelop/StringBuilderArray/stargazers)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

</h3>

The version of StringBuilder built on an array of strings string[]: uses less memory.

## Benchmark:

<details>
  <summary>ToString</summary>
  
```C#

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
            _sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                _sb.AppendLine(_str);
            }

            _sbArr = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                _sbArr.AppendLine(_str);
            }
        }

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            _sb.ToString();
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            _sbArr.ToString();
        }

```
</details>
<br>
<details>
  <summary>AppendLine</summary>
  
```C#

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
        }

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }
        }

```
</details>
<br>
<details>
  <summary>AppendLine + Clear</summary>
  
```C#

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
        }

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }

            sb.Clear();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }

            sb.Clear();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }
        }

```
</details>
<br>
<details>
  <summary>Insert</summary>
  
```C#

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
        }

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sb.Append(_str);
            }

            //before 1 str from end
            sb.Insert(99 * _str.Length, _str);

            //in start
            sb.Insert(0, _str);

            //after 5 str from start
            sb.Insert(5 * _str.Length, _str);
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 100; i++)
            {
                sb.Append(_str);
            }

            sb.Insert(0, _str);

            //in start
            sb.Insert(100, _str);

            //after 5 str from start
            sb.Insert(96, _str);
        }

```
</details>
