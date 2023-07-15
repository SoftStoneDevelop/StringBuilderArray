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
| StringBuilderArray |         5 |      32.89 μs |  1.79 |         - |         - |         - |       34.7 KB |        1.12 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |         **7** |      **20.89 μs** |  **1.00** |         **-** |         **-** |         **-** |      **50.59 KB** |        **1.00** |
| StringBuilderArray |         7 |      32.57 μs |  1.57 |         - |         - |         - |      38.61 KB |        0.76 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |        **10** |      **16.01 μs** |  **1.00** |         **-** |         **-** |         **-** |      **56.45 KB** |        **1.00** |
| StringBuilderArray |        10 |      34.88 μs |  2.23 |         - |         - |         - |      44.47 KB |        0.79 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |        **50** |      **23.38 μs** |  **1.00** |         **-** |         **-** |         **-** |     **206.36 KB** |        **1.00** |
| StringBuilderArray |        50 |      36.02 μs |  1.48 |         - |         - |         - |     122.59 KB |        0.59 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |       **100** |      **27.13 μs** |  **1.00** |         **-** |         **-** |         **-** |      **413.8 KB** |        **1.00** |
| StringBuilderArray |       100 |      43.94 μs |  1.77 |         - |         - |         - |     220.25 KB |        0.53 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |      **1000** |     **735.77 μs** |  **1.00** |         **-** |         **-** |         **-** |    **3935.53 KB** |        **1.00** |
| StringBuilderArray |      1000 |     570.10 μs |  0.80 |         - |         - |         - |    1978.06 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |      **2500** |   **1,790.58 μs** |  **1.00** |         **-** |         **-** |         **-** |    **9804.08 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,412.22 μs |  0.79 |         - |         - |         - |    4907.75 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |      **5000** |   **4,863.92 μs** |  **1.00** | **1000.0000** |         **-** |         **-** |   **19583.76 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,689.05 μs |  0.55 |         - |         - |         - |    9790.56 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |     **10000** |  **10,137.72 μs** |  **1.00** | **2000.0000** | **1000.0000** |         **-** |   **39162.77 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,259.71 μs |  0.51 |         - |         - |         - |   19556.19 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |    **100000** |  **54,801.69 μs** |  **1.00** | **4000.0000** | **4000.0000** | **3000.0000** |  **390802.44 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,538.66 μs |  0.37 |         - |         - |         - |  195337.44 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |    **500000** | **210,721.40 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** | **1953310.77 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,015.80 μs |  0.50 |         - |         - |         - |  976587.44 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **StringBuilder** |   **1071741** | **289,243.13 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** |  **4186664.2 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 152,814.02 μs |  0.53 |         - |         - |         - | 2093269.08 KB |        0.50 |

