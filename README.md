# CSBombmanServer  
  
This project was recreated the following project in C#.  
https://github.com/ha2ne2/BombmanServer

## Client I/O info  

### Client receives string value of JSON object.

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | turn | number | 0 | start from 0 |
|2      | walls | array<array<number>> | (ref) | - |
|3      | players | array<object<Player>> | (ref) | - |
|4      | bombs | array<object<Bomb>> | (ref) | - |
|5      | items | arrasy<<array<number>>> | (ref) | - |
|6      | fires | array<object<Item>> | (ref) | - |

### Client sends string value.

- At first, client sends his name. ex. "テスト"
- Secondary and continuously, client sends his action. ex. "UP, false"

#### Sample of Client.
```
using System;
using System.Text;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.UTF8;
			Console.OutputEncoding = Encoding.UTF8;
			Console.WriteLine("テスト", Console.OutputEncoding.CodePage);
			while (true)
			{
				var s = Console.ReadLine();
				Console.WriteLine("UP,true");
				Console.Out.Flush();
			}
		}
	}
}
```


