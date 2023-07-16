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

|             Method | StrLength |           Mean | Ratio |     Allocated | Alloc Ratio |
|------------------- |---------- |---------------:|------:|--------------:|------------:|
|      **StringBuilder** |         **5** |       **1.045 μs** |  **1.00** |      **14.28 KB** |        **1.00** |
| StringBuilderArray |         5 |      18.980 μs | 18.20 |      14.28 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |         **7** |       **1.658 μs** |  **1.00** |      **18.19 KB** |        **1.00** |
| StringBuilderArray |         7 |      18.900 μs | 11.91 |      18.19 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |        **10** |       **1.445 μs** |  **1.00** |      **24.05 KB** |        **1.00** |
| StringBuilderArray |        10 |      19.863 μs | 13.96 |      24.05 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |        **50** |       **7.270 μs** |  **1.00** |     **102.17 KB** |        **1.00** |
| StringBuilderArray |        50 |      21.000 μs |  3.43 |     102.17 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |       **100** |       **7.833 μs** |  **1.00** |     **199.83 KB** |        **1.00** |
| StringBuilderArray |       100 |      30.762 μs |  4.27 |     199.83 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |      **1000** |     **492.531 μs** |  **1.00** |    **1957.64 KB** |        **1.00** |
| StringBuilderArray |      1000 |     542.429 μs |  1.11 |    1957.64 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |      **2500** |   **1,401.836 μs** |  **1.00** |    **4887.33 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,362.585 μs |  0.98 |    4887.33 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |      **5000** |   **2,924.100 μs** |  **1.00** |    **9770.14 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,786.989 μs |  0.98 |    9770.14 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |     **10000** |   **5,630.200 μs** |  **1.00** |   **19535.77 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,176.920 μs |  0.92 |   19535.77 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |    **100000** |  **26,069.379 μs** |  **1.00** |  **195317.02 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,565.615 μs |  0.80 |  195317.02 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |    **500000** | **124,607.154 μs** |  **1.00** |  **976567.02 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,281.508 μs |  0.84 |  976567.02 KB |        1.00 |
|                    |           |                |       |               |             |
|      **StringBuilder** |   **1071741** | **192,839.555 μs** |  **1.00** | **2093248.66 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 153,743.317 μs |  0.80 | 2093248.66 KB |        1.00 |
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

|             Method | StrLength |         Mean | Ratio |     Allocated | Alloc Ratio |
|------------------- |---------- |-------------:|------:|--------------:|------------:|
|      **StringBuilder** |         **5** |     **16.62 μs** |  **1.00** |      **17.29 KB** |        **1.00** |
| StringBuilderArray |         5 |     16.81 μs |  1.02 |      21.07 KB |        1.22 |
|                    |           |              |       |               |             |
|      **StringBuilder** |         **7** |     **17.74 μs** |  **1.00** |      **32.98 KB** |        **1.00** |
| StringBuilderArray |         7 |     15.84 μs |  0.90 |      21.07 KB |        0.64 |
|                    |           |              |       |               |             |
|      **StringBuilder** |        **10** |     **17.89 μs** |  **1.00** |      **32.98 KB** |        **1.00** |
| StringBuilderArray |        10 |     15.82 μs |  0.86 |      21.07 KB |        0.64 |
|                    |           |              |       |               |             |
|      **StringBuilder** |        **50** |     **19.85 μs** |  **1.00** |     **104.77 KB** |        **1.00** |
| StringBuilderArray |        50 |     15.84 μs |  0.80 |      21.07 KB |        0.20 |
|                    |           |              |       |               |             |
|      **StringBuilder** |       **100** |     **28.90 μs** |  **1.00** |     **214.56 KB** |        **1.00** |
| StringBuilderArray |       100 |     15.95 μs |  0.59 |      21.07 KB |        0.10 |
|                    |           |              |       |               |             |
|      **StringBuilder** |      **1000** |     **95.42 μs** |  **1.00** |    **1978.48 KB** |        **1.00** |
| StringBuilderArray |      1000 |     15.80 μs |  0.17 |      21.07 KB |        0.01 |
|                    |           |              |       |               |             |
|      **StringBuilder** |      **2500** |    **404.60 μs** |  **1.00** |    **4917.34 KB** |       **1.000** |
| StringBuilderArray |      2500 |     16.11 μs |  0.05 |      21.07 KB |       0.004 |
|                    |           |              |       |               |             |
|      **StringBuilder** |      **5000** |  **1,825.35 μs** | **1.000** |     **9814.2 KB** |       **1.000** |
| StringBuilderArray |      5000 |     16.75 μs | 0.009 |      21.07 KB |       0.002 |
|                    |           |              |       |               |             |
|      **StringBuilder** |     **10000** |  **2,750.18 μs** | **1.000** |   **19627.59 KB** |       **1.000** |
| StringBuilderArray |     10000 |     15.68 μs | 0.006 |      21.07 KB |       0.001 |
|                    |           |              |       |               |             |
|      **StringBuilder** |    **100000** | **29,660.86 μs** | **1.000** |  **195486.03 KB** |       **1.000** |
| StringBuilderArray |    100000 |     16.90 μs | 0.001 |      21.07 KB |       0.000 |
|                    |           |              |       |               |             |
|      **StringBuilder** |    **500000** | **88,897.48 μs** | **1.000** |  **976744.34 KB** |       **1.000** |
| StringBuilderArray |    500000 |     16.94 μs | 0.000 |      21.07 KB |       0.000 |
|                    |           |              |       |               |             |
|      **StringBuilder** |   **1071741** | **99,606.53 μs** | **1.000** | **2093416.13 KB** |       **1.000** |
| StringBuilderArray |   1071741 |     17.27 μs | 0.000 |      21.07 KB |       0.000 |
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

|             Method | StrLength |          Mean | Ratio |     Allocated | Alloc Ratio |
|------------------- |---------- |--------------:|------:|--------------:|------------:|
|      **StringBuilder** |         **5** |      **22.59 μs** |  **1.00** |      **33.31 KB** |        **1.00** |
| StringBuilderArray |         5 |      21.73 μs |  0.97 |      41.41 KB |        1.24 |
|                    |           |               |       |               |             |
|      **StringBuilder** |         **7** |      **21.33 μs** |  **1.00** |       **54.1 KB** |        **1.00** |
| StringBuilderArray |         7 |      22.23 μs |  1.04 |      41.41 KB |        0.77 |
|                    |           |               |       |               |             |
|      **StringBuilder** |        **10** |      **22.01 μs** |  **1.00** |      **61.13 KB** |        **1.00** |
| StringBuilderArray |        10 |      20.50 μs |  0.96 |      41.41 KB |        0.68 |
|                    |           |               |       |               |             |
|      **StringBuilder** |        **50** |      **29.93 μs** |  **1.00** |     **207.92 KB** |        **1.00** |
| StringBuilderArray |        50 |      21.05 μs |  0.71 |      41.41 KB |        0.20 |
|                    |           |               |       |               |             |
|      **StringBuilder** |       **100** |      **31.54 μs** |  **1.00** |     **427.09 KB** |        **1.00** |
| StringBuilderArray |       100 |      20.36 μs |  0.64 |      41.41 KB |        0.10 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **1000** |     **606.54 μs** |  **1.00** |    **3947.25 KB** |        **1.00** |
| StringBuilderArray |      1000 |      21.36 μs |  0.04 |      41.41 KB |        0.01 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **2500** |   **1,686.58 μs** |  **1.00** |    **9811.89 KB** |       **1.000** |
| StringBuilderArray |      2500 |      20.51 μs |  0.01 |      41.41 KB |       0.004 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **5000** |   **4,623.29 μs** | **1.000** |   **19583.76 KB** |       **1.000** |
| StringBuilderArray |      5000 |      21.13 μs | 0.005 |      41.41 KB |       0.002 |
|                    |           |               |       |               |             |
|      **StringBuilder** |     **10000** |   **8,393.39 μs** | **1.000** |   **39166.66 KB** |       **1.000** |
| StringBuilderArray |     10000 |      20.46 μs | 0.002 |      41.41 KB |       0.001 |
|                    |           |               |       |               |             |
|      **StringBuilder** |    **100000** |  **42,374.85 μs** | **1.000** |  **390818.06 KB** |       **1.000** |
| StringBuilderArray |    100000 |      21.80 μs | 0.001 |      41.41 KB |       0.000 |
|                    |           |               |       |               |             |
|      **StringBuilder** |    **500000** | **163,509.21 μs** | **1.000** | **1953470.91 KB** |       **1.000** |
| StringBuilderArray |    500000 |      20.45 μs | 0.000 |      41.41 KB |       0.000 |
|                    |           |               |       |               |             |
|      **StringBuilder** |   **1071741** | **184,949.84 μs** | **1.000** | **4186765.65 KB** |       **1.000** |
| StringBuilderArray |   1071741 |      21.60 μs | 0.000 |      41.41 KB |       0.000 |
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

|             Method | StrLength |          Mean | Ratio |    Allocated | Alloc Ratio |
|------------------- |---------- |--------------:|------:|-------------:|------------:|
|      **StringBuilder** |         **5** |      **1.962 μs** |  **1.00** |      **2.31 KB** |        **1.00** |
| StringBuilderArray |         5 |      3.721 μs |  1.88 |      2.04 KB |        0.88 |
|                    |           |               |       |              |             |
|      **StringBuilder** |         **7** |      **2.079 μs** |  **1.00** |      **3.38 KB** |        **1.00** |
| StringBuilderArray |         7 |      3.737 μs |  1.84 |      2.04 KB |        0.60 |
|                    |           |               |       |              |             |
|      **StringBuilder** |        **10** |      **2.153 μs** |  **1.00** |      **3.38 KB** |        **1.00** |
| StringBuilderArray |        10 |      4.000 μs |  1.87 |      2.04 KB |        0.60 |
|                    |           |               |       |              |             |
|      **StringBuilder** |        **50** |      **3.168 μs** |  **1.00** |     **14.24 KB** |        **1.00** |
| StringBuilderArray |        50 |      3.861 μs |  1.26 |      2.04 KB |        0.14 |
|                    |           |               |       |              |             |
|      **StringBuilder** |       **100** |      **3.164 μs** |  **1.00** |     **27.02 KB** |        **1.00** |
| StringBuilderArray |       100 |      4.023 μs |  1.26 |      2.04 KB |        0.08 |
|                    |           |               |       |              |             |
|      **StringBuilder** |      **1000** |     **10.162 μs** |  **1.00** |    **210.98 KB** |       **1.000** |
| StringBuilderArray |      1000 |      3.784 μs |  0.37 |      2.04 KB |       0.010 |
|                    |           |               |       |              |             |
|      **StringBuilder** |      **2500** |     **20.950 μs** |  **1.00** |    **506.12 KB** |       **1.000** |
| StringBuilderArray |      2500 |      3.828 μs |  0.18 |      2.04 KB |       0.004 |
|                    |           |               |       |              |             |
|      **StringBuilder** |      **5000** |     **40.233 μs** |  **1.00** |   **1022.95 KB** |       **1.000** |
| StringBuilderArray |      5000 |      3.850 μs |  0.10 |      2.04 KB |       0.002 |
|                    |           |               |       |              |             |
|      **StringBuilder** |     **10000** |     **71.107 μs** |  **1.00** |   **2019.62 KB** |       **1.000** |
| StringBuilderArray |     10000 |      3.971 μs |  0.06 |      2.04 KB |       0.001 |
|                    |           |               |       |              |             |
|      **StringBuilder** |    **100000** |  **5,580.048 μs** | **1.000** |  **20133.65 KB** |       **1.000** |
| StringBuilderArray |    100000 |      3.967 μs | 0.001 |      2.04 KB |       0.000 |
|                    |           |               |       |              |             |
|      **StringBuilder** |    **500000** | **13,838.945 μs** | **1.000** |  **100610.3 KB** |       **1.000** |
| StringBuilderArray |    500000 |      3.982 μs | 0.000 |      2.04 KB |       0.000 |
|                    |           |               |       |              |             |
|      **StringBuilder** |   **1071741** | **15,602.540 μs** | **1.000** | **215621.21 KB** |       **1.000** |
| StringBuilderArray |   1071741 |      3.875 μs | 0.000 |      2.04 KB |       0.000 |
</details>
