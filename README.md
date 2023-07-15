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

```C#

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
|      **System.Text.StringBuilder** |         **5** |      **17.97 μs** |  **1.00** |         **-** |         **-** |         **-** |      **30.98 KB** |        **1.00** |
| StringBuilderArray |         5 |      31.92 μs |  1.77 |         - |         - |         - |      34.77 KB |        1.12 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |         **7** |      **19.56 μs** |  **1.00** |         **-** |         **-** |         **-** |      **50.59 KB** |        **1.00** |
| StringBuilderArray |         7 |      31.61 μs |  1.64 |         - |         - |         - |      38.67 KB |        0.76 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |        **10** |      **18.31 μs** |  **1.00** |         **-** |         **-** |         **-** |      **56.45 KB** |        **1.00** |
| StringBuilderArray |        10 |      32.29 μs |  1.76 |         - |         - |         - |      44.53 KB |        0.79 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |        **50** |      **28.05 μs** |  **1.00** |         **-** |         **-** |         **-** |     **206.36 KB** |        **1.00** |
| StringBuilderArray |        50 |      34.38 μs |  1.39 |         - |         - |         - |     122.66 KB |        0.59 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |       **100** |      **29.47 μs** |  **1.00** |         **-** |         **-** |         **-** |      **413.8 KB** |        **1.00** |
| StringBuilderArray |       100 |      37.92 μs |  1.28 |         - |         - |         - |     220.31 KB |        0.53 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |      **1000** |     **747.71 μs** |  **1.00** |         **-** |         **-** |         **-** |    **3935.53 KB** |        **1.00** |
| StringBuilderArray |      1000 |     550.49 μs |  0.78 |         - |         - |         - |    1978.13 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |      **2500** |   **1,869.84 μs** |  **1.00** |         **-** |         **-** |         **-** |    **9804.08 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,348.66 μs |  0.73 |         - |         - |         - |    4907.81 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |      **5000** |   **5,274.39 μs** |  **1.00** | **1000.0000** |         **-** |         **-** |   **19583.76 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,672.41 μs |  0.51 |         - |         - |         - |    9790.63 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |     **10000** |  **10,141.16 μs** |  **1.00** | **2000.0000** | **1000.0000** |         **-** |   **39162.77 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,112.69 μs |  0.50 |         - |         - |         - |   19556.25 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |    **100000** |  **54,063.39 μs** |  **1.00** | **4000.0000** | **4000.0000** | **3000.0000** |  **390802.44 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,596.38 μs |  0.38 |         - |         - |         - |   195337.5 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |    **500000** | **210,668.68 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** | **1953310.77 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,116.28 μs |  0.50 |         - |         - |         - |   976587.5 KB |        0.50 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |   **1071741** | **289,865.89 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** |  **4186664.2 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 152,856.84 μs |  0.53 |         - |         - |         - | 2093269.14 KB |        0.50 |

## Reuse instance benchmark
```C#

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

            sb.Clear();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }

            var result = sb.ToString();
        }

```

|             Method | StrLength |          Mean | Ratio |      Gen0 |      Gen1 |      Gen2 |     Allocated | Alloc Ratio |
|------------------- |---------- |--------------:|------:|----------:|----------:|----------:|--------------:|------------:|
|      **System.Text.StringBuilder** |         **5** |      **22.54 μs** |  **1.00** |         **-** |         **-** |         **-** |      **47.01 KB** |        **1.00** |
| StringBuilderArray |         5 |      39.13 μs |  1.75 |         - |         - |         - |      45.12 KB |        0.96 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |         **7** |      **21.42 μs** |  **1.00** |         **-** |         **-** |         **-** |       **71.7 KB** |        **1.00** |
| StringBuilderArray |         7 |      38.15 μs |  1.78 |         - |         - |         - |      49.02 KB |        0.68 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |        **10** |      **22.49 μs** |  **1.00** |         **-** |         **-** |         **-** |      **84.59 KB** |        **1.00** |
| StringBuilderArray |        10 |      39.34 μs |  1.77 |         - |         - |         - |      54.88 KB |        0.65 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |        **50** |      **29.06 μs** |  **1.00** |         **-** |         **-** |         **-** |     **309.51 KB** |        **1.00** |
| StringBuilderArray |        50 |      41.42 μs |  1.42 |         - |         - |         - |     133.01 KB |        0.43 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |       **100** |      **96.25 μs** |  **1.00** |         **-** |         **-** |         **-** |     **626.33 KB** |        **1.00** |
| StringBuilderArray |       100 |      49.48 μs |  0.56 |         - |         - |         - |     230.66 KB |        0.37 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |      **1000** |   **1,109.48 μs** |  **1.00** |         **-** |         **-** |         **-** |     **5904.3 KB** |        **1.00** |
| StringBuilderArray |      1000 |     542.58 μs |  0.50 |         - |         - |         - |    1988.48 KB |        0.34 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |      **2500** |   **3,839.15 μs** |  **1.00** | **1000.0000** | **1000.0000** | **1000.0000** |   **14698.94 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,378.40 μs |  0.36 |         - |         - |         - |    4918.16 KB |        0.33 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |      **5000** |   **8,019.07 μs** |  **1.00** | **2000.0000** | **1000.0000** | **1000.0000** |   **29353.62 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,711.04 μs |  0.36 |         - |         - |         - |    9800.98 KB |        0.33 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |     **10000** |  **13,084.87 μs** |  **1.00** | **3000.0000** | **2000.0000** | **1000.0000** |   **58702.15 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,610.82 μs |  0.42 |         - |         - |         - |    19566.6 KB |        0.33 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |    **100000** |  **95,966.62 μs** |  **1.00** | **5000.0000** | **5000.0000** | **4000.0000** |  **586134.84 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,813.11 μs |  0.25 |         - |         - |         - |  195347.85 KB |        0.33 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |    **500000** | **284,075.06 μs** |  **1.00** | **7000.0000** | **6000.0000** | **4000.0000** | **2930037.34 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,142.19 μs |  0.37 |         - |         - |         - |  976597.85 KB |        0.33 |
|                    |           |               |       |           |           |           |               |             |
|      **System.Text.StringBuilder** |   **1071741** | **370,876.76 μs** |  **1.00** | **7000.0000** | **6000.0000** | **4000.0000** | **6280013.72 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 152,671.59 μs |  0.41 |         - |         - |         - | 2093279.49 KB |        0.33 |
