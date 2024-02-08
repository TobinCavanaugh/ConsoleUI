## Console.BufferWidth & Console.BufferHeight

Getting `Console.BufferWidth` and `Console.BufferHeight`
10k iterations

|       |       |       |       |       |       |       |       |       |       |       |       |       |       |       |       |       |
|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|
| 671ms | 608ms | 636ms | 617ms | 589ms | 553ms | 566ms | 564ms | 527ms | 529ms | 528ms | 465ms | 520ms | 486ms | 579ms | 618ms | 757ms |

<details>

```C#
while (true)
{
    Stopwatch super = new();
    super.Start();
    for (int i = 0; i < 10000; i++)
    {
        var dimX = Console.BufferWidth;
        var dimY = Console.BufferHeight;
    }

    super.Stop();
    Console.WriteLine(super.ElapsedMilliseconds + "ms");
}
```

</details>

---
## Console.SetCursorPosition v. Console.CursorLeft & Console.CursorTop
#### 10k iterations

**[0]** `Console.SetCursorPosition`


|       |       |       |       |       |       |       |       |       |       |       |       |       |       |       |
|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|:-----:|
|274ms|265ms|266ms|294ms|285ms|273ms|260ms|250ms|253ms|251ms|255ms|245ms|249ms|268ms|267ms|

**[1]** `Console.CursorLeft` & `Console.CursorTop`

|       |        |        |        |        |        |        |        |        |        |
|:-----:|:------:|:------:|:------:|:------:|:------:|:------:|:------:|:------:|:------:|
| 975ms | 1004ms | 1101ms | 1171ms | 1133ms | 1200ms | 1234ms | 1172ms | 1130ms | 1236ms |1322ms|


<details>

```C#

var dim = (Console.BufferWidth, Console.BufferHeight);
int it = 0;
while (true)
{
    Stopwatch super = new();
    Random r = new Random();
    super.Start();
    for (int i = 0; i < 10000; i++)
    {
        //[0] Console.SetCursorPosition(r.Next(dim.BufferWidth), r.Next(dim.BufferHeight));
        //[1] Console.CursorLeft = r.Next(dim.BufferWidth);
        //[1] Console.CursorTop = r.Next(dim.BufferHeight);
    }

    super.Stop();
    Console.SetCursorPosition(0, it);
    Console.WriteLine(super.ElapsedMilliseconds + "ms");
    it++;
}
```

</details>


---

## SafeSet v. Direct Indexing
### 10k iterations

