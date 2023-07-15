<h1 align="center">
  <a>StringBuilderArray</a>
</h1>

<h3 align="center">

  [![Nuget](https://img.shields.io/nuget/v/Gedaq.Npgsql?logo=StringBuilderArray)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Downloads](https://img.shields.io/nuget/dt/StringBuilderArray.svg)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Stars](https://img.shields.io/github/stars/SoftStoneDevelop/StringBuilderArray?color=brightgreen)](https://github.com/SoftStoneDevelop/StringBuilderArray/stargazers)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

</h3>

The version of StringBuilder built on an array of strings string[]: uses less memory.

Benchmark:

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

            var result = sb.ToString();
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }

            var result = sb.ToString();
        }

```

|             Method | StrLength |          Mean | Ratio |      Gen0 |      Gen1 |      Gen2 |     Allocated | Alloc Ratio |
|------------------- |---------- |--------------:|------:|----------:|----------:|----------:|--------------:|------------:|
|      **StringBuilder** |         **5** |      **18.40 μs** |  **1.00** |         **-** |         **-** |         **-** |      **30.98 KB** |        **1.00** |
| StringBuilderArray |         5 |      33.35 μs |  1.83 |         - |         - |         - |       34.7 KB |        1.12 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |         **7** |      **16.46 μs** |  **1.00** |         **-** |         **-** |         **-** |      **50.59 KB** |        **1.00** |
| StringBuilderArray |         7 |      32.61 μs |  2.02 |         - |         - |         - |      38.61 KB |        0.76 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |        **10** |      **17.37 μs** |  **1.00** |         **-** |         **-** |         **-** |      **56.45 KB** |        **1.00** |
| StringBuilderArray |        10 |      33.83 μs |  1.98 |         - |         - |         - |      44.47 KB |        0.79 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |        **50** |      **22.60 μs** |  **1.00** |         **-** |         **-** |         **-** |     **206.36 KB** |        **1.00** |
| StringBuilderArray |        50 |      35.55 μs |  1.61 |         - |         - |         - |     122.59 KB |        0.59 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |       **100** |      **28.69 μs** |  **1.00** |         **-** |         **-** |         **-** |      **413.8 KB** |        **1.00** |
| StringBuilderArray |       100 |      42.39 μs |  1.49 |         - |         - |         - |     220.25 KB |        0.53 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |      **1000** |     **655.56 μs** |  **1.00** |         **-** |         **-** |         **-** |    **3935.53 KB** |        **1.00** |
| StringBuilderArray |      1000 |     572.79 μs |  0.90 |         - |         - |         - |    1978.06 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |      **2500** |   **1,937.56 μs** |  **1.00** |         **-** |         **-** |         **-** |    **9804.08 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,344.46 μs |  0.70 |         - |         - |         - |    4907.75 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |      **5000** |   **4,863.25 μs** |  **1.00** | **1000.0000** |         **-** |         **-** |   **19583.76 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,893.15 μs |  0.60 |         - |         - |         - |    9790.56 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |     **10000** |  **10,066.64 μs** |  **1.00** | **2000.0000** | **1000.0000** |         **-** |   **39162.77 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,164.62 μs |  0.52 |         - |         - |         - |   19556.19 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |    **100000** |  **54,942.42 μs** |  **1.00** | **4000.0000** | **4000.0000** | **3000.0000** |  **390802.44 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,573.11 μs |  0.37 |         - |         - |         - |  195337.44 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |    **500000** | **212,154.11 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** | **1953310.77 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,018.88 μs |  0.49 |         - |         - |         - |  976587.44 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |   **1071741** | **288,174.29 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** |  **4186664.2 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 153,042.51 μs |  0.53 |         - |         - |         - | 2093269.08 KB |        0.50 |