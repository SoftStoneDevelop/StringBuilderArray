<h1 align="center">
  <a>StringBuilderArray</a>
</h1>

<h3 align="center">

  [![Nuget](https://img.shields.io/nuget/v/StringBuilderArray?logo=StringBuilderArray)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Downloads](https://img.shields.io/nuget/dt/StringBuilderArray.svg)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Stars](https://img.shields.io/github/stars/SoftStoneDevelop/StringBuilderArray?color=brightgreen)](https://github.com/SoftStoneDevelop/StringBuilderArray/stargazers)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

</h3>

Collect the resulting string without allocating memory.

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

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
public void StringBuilderArray()
{
    _sbArr.ToString();
}

[Benchmark(Description = "StringBuilder")]
public void StringBuilder()
{
    _sb.ToString();
}

```

| Method             | Job            | Runtime        | StrLength | Mean           | Ratio | Allocated     | Alloc Ratio |
|------------------- |--------------- |--------------- |---------- |---------------:|------:|--------------:|------------:|
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **33.669 μs** |  **1.01** |       **13.7 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 5         |       7.276 μs |  0.22 |       13.7 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       7.282 μs |  1.00 |       13.7 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       1.231 μs |  0.17 |       13.7 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1071741**   | **289,587.367 μs** |  **1.00** | **2093248.07 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1071741   | 318,024.700 μs |  1.10 | 2093248.07 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 292,666.936 μs |  1.00 | 2093248.07 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 320,468.233 μs |  1.10 | 2093248.07 KB |        1.00 |
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

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
public void StringBuilderArray()
{
    var sb = new StringBuilderArray.StringBuilderArray();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }
}

[Benchmark(Description = "StringBuilder")]
public void StringBuilder()
{
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }
}

[Benchmark(Description = "DefaultInterpolatedStringHandler")]
public void DefaultInterpolatedStringHandler()
{
    var sb = new DefaultInterpolatedStringHandler();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
    }
}

```

| Method                           | Job            | Runtime        | StrLength | Mean           | Ratio     | Allocated     | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |---------------:|----------:|--------------:|------------:|
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **28.829 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 5         |      28.224 μs |      0.99 |       16.7 KB |        0.82 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5         |      22.851 μs |      0.80 |      16.02 KB |        0.78 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       4.460 μs |      1.01 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       7.596 μs |      1.72 |      17.36 KB |        0.85 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       4.743 μs |      1.07 |      16.02 KB |        0.78 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **29.945 μs** |      **1.03** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1071741   | 123,382.045 μs |  4,229.27 | 2093438.98 KB |  102,196.87 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1071741   | 231,279.579 μs |  7,927.77 | 2097152.02 KB |  102,378.13 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |       6.277 μs |      1.02 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 126,442.383 μs | 20,631.07 | 2093440.23 KB |  102,196.93 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 227,531.679 μs | 37,125.38 |  2097152.4 KB |  102,378.15 |
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

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
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

[Benchmark(Description = "StringBuilder")]
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

[Benchmark(Description = "DefaultInterpolatedStringHandler")]
public void DefaultInterpolatedStringHandler()
{
    var sb = new DefaultInterpolatedStringHandler();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
    }

    sb.Clear();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
    }
}

```

| Method                           | Job            | Runtime        | StrLength | Mean           | Ratio     | Allocated     | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |---------------:|----------:|--------------:|------------:|
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **43.771 μs** |      **1.03** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 5         |      39.806 μs |      0.94 |      32.73 KB |        0.80 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5         |      27.016 μs |      0.63 |      16.02 KB |        0.39 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       9.252 μs |      1.00 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |      12.936 μs |      1.40 |      32.73 KB |        0.80 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       7.050 μs |      0.76 |      16.02 KB |        0.39 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **39.325 μs** |      **1.03** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1071741   | 233,685.746 μs |  6,141.00 | 4186788.48 KB |  102,566.30 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1071741   | 466,345.020 μs | 12,255.03 | 2097152.02 KB |   51,375.21 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |      10.092 μs |      1.01 |      40.87 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 240,078.979 μs | 23,996.71 | 4186789.73 KB |  102,448.69 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 455,444.213 μs | 45,523.19 | 2097152.02 KB |   51,316.28 |
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

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
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

[Benchmark(Description = "StringBuilder")]
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

```

| Method             | Job            | Runtime        | StrLength | Mean          | Ratio    | Allocated    | Alloc Ratio |
|------------------- |--------------- |--------------- |---------- |--------------:|---------:|-------------:|------------:|
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **5.328 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 5         |      3.128 μs |     0.60 |      1.73 KB |        1.19 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 5         |      2.038 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 5         |      1.795 μs |     0.89 |      1.73 KB |        1.19 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **5.069 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1071741   | 14,424.515 μs | 2,909.17 | 215628.59 KB |  148,389.57 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |      2.073 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 13,324.245 μs | 6,492.04 | 215629.18 KB |  148,389.97 |

</details>
