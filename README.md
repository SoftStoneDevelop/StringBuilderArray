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

|             Method | StrLength |          Mean | Ratio |     Allocated | Alloc Ratio |
|------------------- |---------- |--------------:|------:|--------------:|------------:|
|      **StringBuilder** |         **5** |      **17.89 μs** |  **1.00** |      **30.98 KB** |        **1.00** |
| StringBuilderArray |         5 |      33.04 μs |  1.84 |      34.77 KB |        1.12 |
|                    |           |               |       |               |             |
|      **StringBuilder** |         **7** |      **19.36 μs** |  **1.00** |      **50.59 KB** |        **1.00** |
| StringBuilderArray |         7 |      33.03 μs |  1.71 |      38.67 KB |        0.76 |
|                    |           |               |       |               |             |
|      **StringBuilder** |        **10** |      **19.20 μs** |  **1.00** |      **56.45 KB** |        **1.00** |
| StringBuilderArray |        10 |      33.44 μs |  1.75 |      44.53 KB |        0.79 |
|                    |           |               |       |               |             |
|      **StringBuilder** |        **50** |      **27.21 μs** |  **1.00** |     **206.36 KB** |        **1.00** |
| StringBuilderArray |        50 |      35.86 μs |  1.25 |     122.66 KB |        0.59 |
|                    |           |               |       |               |             |
|      **StringBuilder** |       **100** |      **29.57 μs** |  **1.00** |      **413.8 KB** |        **1.00** |
| StringBuilderArray |       100 |      39.34 μs |  1.33 |     220.31 KB |        0.53 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **1000** |     **701.28 μs** |  **1.00** |    **3935.53 KB** |        **1.00** |
| StringBuilderArray |      1000 |     587.01 μs |  0.89 |    1978.13 KB |        0.50 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **2500** |   **1,840.94 μs** |  **1.00** |    **9804.08 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,374.07 μs |  0.75 |    4907.81 KB |        0.50 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **5000** |   **5,041.05 μs** |  **1.00** |   **19583.76 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,795.59 μs |  0.56 |    9790.95 KB |        0.50 |
|                    |           |               |       |               |             |
|      **StringBuilder** |     **10000** |   **9,842.60 μs** |  **1.00** |   **39162.77 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,271.43 μs |  0.53 |   19556.25 KB |        0.50 |
|                    |           |               |       |               |             |
|      **StringBuilder** |    **100000** |  **54,744.65 μs** |  **1.00** |  **390802.46 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,572.29 μs |  0.38 |   195337.5 KB |        0.50 |
|                    |           |               |       |               |             |
|      **StringBuilder** |    **500000** | **213,324.82 μs** |  **1.00** | **1953310.77 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,173.60 μs |  0.49 |   976587.5 KB |        0.50 |
|                    |           |               |       |               |             |
|      **StringBuilder** |   **1071741** | **289,821.51 μs** |  **1.00** |  **4186664.2 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 153,198.15 μs |  0.53 | 2093269.14 KB |        0.50 |

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

|             Method | StrLength |          Mean | Ratio |     Allocated | Alloc Ratio |
|------------------- |---------- |--------------:|------:|--------------:|------------:|
|      **StringBuilder** |         **5** |      **25.98 μs** |  **1.00** |      **47.01 KB** |        **1.00** |
| StringBuilderArray |         5 |      39.27 μs |  1.54 |       55.1 KB |        1.17 |
|                    |           |               |       |               |             |
|      **StringBuilder** |         **7** |      **23.70 μs** |  **1.00** |       **71.7 KB** |        **1.00** |
| StringBuilderArray |         7 |      40.51 μs |  1.72 |      59.01 KB |        0.82 |
|                    |           |               |       |               |             |
|      **StringBuilder** |        **10** |      **25.20 μs** |  **1.00** |      **84.59 KB** |        **1.00** |
| StringBuilderArray |        10 |      39.08 μs |  1.60 |      64.87 KB |        0.77 |
|                    |           |               |       |               |             |
|      **StringBuilder** |        **50** |      **30.39 μs** |  **1.00** |     **309.51 KB** |        **1.00** |
| StringBuilderArray |        50 |      45.60 μs |  1.52 |     142.99 KB |        0.46 |
|                    |           |               |       |               |             |
|      **StringBuilder** |       **100** |     **108.48 μs** |  **1.00** |     **626.33 KB** |        **1.00** |
| StringBuilderArray |       100 |      44.93 μs |  0.45 |     240.65 KB |        0.38 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **1000** |   **1,179.51 μs** |  **1.00** |     **5904.3 KB** |        **1.00** |
| StringBuilderArray |      1000 |     570.91 μs |  0.49 |    1998.46 KB |        0.34 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **2500** |   **3,970.17 μs** |  **1.00** |   **14698.94 KB** |        **1.00** |
| StringBuilderArray |      2500 |   1,516.39 μs |  0.38 |    4928.15 KB |        0.34 |
|                    |           |               |       |               |             |
|      **StringBuilder** |      **5000** |   **8,286.17 μs** |  **1.00** |   **29353.62 KB** |        **1.00** |
| StringBuilderArray |      5000 |   2,903.21 μs |  0.35 |    9810.96 KB |        0.33 |
|                    |           |               |       |               |             |
|      **StringBuilder** |     **10000** |  **13,094.03 μs** |  **1.00** |   **58702.15 KB** |        **1.00** |
| StringBuilderArray |     10000 |   5,196.08 μs |  0.40 |   19576.59 KB |        0.33 |
|                    |           |               |       |               |             |
|      **StringBuilder** |    **100000** |  **66,915.82 μs** |  **1.00** |  **586134.84 KB** |        **1.00** |
| StringBuilderArray |    100000 |  20,600.99 μs |  0.31 |  195357.84 KB |        0.33 |
|                    |           |               |       |               |             |
|      **StringBuilder** |    **500000** | **284,550.02 μs** |  **1.00** | **2930037.34 KB** |        **1.00** |
| StringBuilderArray |    500000 | 105,025.78 μs |  0.37 |  976607.84 KB |        0.33 |
|                    |           |               |       |               |             |
|      **StringBuilder** |   **1071741** | **370,070.86 μs** |  **1.00** | **6280013.72 KB** |        **1.00** |
| StringBuilderArray |   1071741 | 153,051.83 μs |  0.41 | 2093289.48 KB |        0.33 |

Insert benchmark

```C#

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
