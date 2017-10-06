# CSBombmanServer  
  
This project was recreated the following project in C#.  
https://github.com/ha2ne2/BombmanServer

## Client I/O info  

### Client receives string value of JSON object.

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | turn | number | 0 | start from 0 |
|2      | walls | array<array<number>> | see [sample](#SampleJSONFromServer) | In walls and blocks, Position is expressed simply as a numerical array. [1, 5] is expressed as {"x": 1, "y": 5} by Position notation. |
|3      | blocks | array<array<number>> | see [sample](#SampleJSONFromServer) | In walls and blocks, Position is expressed simply as a numerical array. [1, 5] is expressed as {"x": 1, "y": 5} by Position notation. |
|4      | players | array<object<[Player](#Player)>> | see [sample](#SampleJSONFromServer) | - |
|5      | bombs | array<object<[Bomb](#Bomb)>> | see [sample](#SampleJSONFromServer) | - |
|6      | items | array<<array<number>>> | see [sample](#SampleJSONFromServer) | - |
|7      | fires | array<object<[Item](#Item)>> | see [sample](#SampleJSONFromServer) | - |

##### Player

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | name | string | あなた | player name |
|2      | pos | [Position](#Position) | {"x":1, "y":1} | position of player |
|3      | power | number | 2 | bom's firepower installed by the player |
|4      | ch | string | あ | the first character of player name |
|5      | isAlive | boolean | true | if player is alive, true, otherwise, false |
|6      | setBombCount | number | 0 | number of bombs which is set by player and the bomb has not been burned. |
|7      | totalSetBombCount | number | 0 | number of bombs which is set by player |
|8      | id | number | 0 | any one of 0 to 3 |

##### Bomb

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | pos | [Position](#Position) | {"x":1, "y":1} | position of bomb |
|2      | timer | number | 9 | Remaining turn to explode. The timer starts from 9 and bomb is exploded in 0. |
|3      | power | number | 2 | Bom's firepower |

##### Item

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | pos | [Position](#Position) | {"x":1, "y":1} | Position of item. When an item appears, it is at the same position as fire. |
|2      | name | string | 弾 | tither one of "弾" and "力" |

##### Position

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | x | number | 1 | x is the horizontal coordinate. The wall on the left is 0 and the wall on the right is 14. |
|2      | y | number | 1 | y is the vertical coordinate. The top wall is 0 and the bottom wall is 14. |

##### Player

|(order)| key  | type | sample | memo|
|-------| ---- | ---- | ------ | --- |
|1      | name | string | あなた | player name |
|2      | pos | [Position](#Position) | {"x":1, "y":1} | position of player |
|3      | power | number | 2 | Bom's firepower installed by the player |
|4      | ch | string | あ | the first character of player name |
|5      | isAlive | boolean | true | if player is alive, true, otherwise, false |
|6      | setBombCount | number | 0 | number of bombs which is set by player and the bomb has not been burned. |
|6      | totalSetBombCount | number | 0 | number of bombs which is set by player |
|6      | id | number | 0 | any one of 0 to 3 |


#### SampleJSONFromServer

###### Turn29

![Turn20](https://raw.githubusercontent.com/goto13/CSBombmanServer/image/images/turn20.png "Turn20")

```
{"turn":20,"walls":[[0,0],[0,1],[0,2],[0,3],[0,4],[0,5],[0,6],[0,7],[0,8],[0,9],[0,10],[0,11],[0,12],[0,13],[0,14],[1,0],[1,14],[2,0],[2,2],[2,4],[2,6],[2,8],[2,10],[2,12],[2,14],[3,0],[3,14],[4,0],[4,2],[4,4],[4,6],[4,8],[4,10],[4,12],[4,14],[5,0],[5,14],[6,0],[6,2],[6,4],[6,6],[6,8],[6,10],[6,12],[6,14],[7,0],[7,14],[8,0],[8,2],[8,4],[8,6],[8,8],[8,10],[8,12],[8,14],[9,0],[9,14],[10,0],[10,2],[10,4],[10,6],[10,8],[10,10],[10,12],[10,14],[11,0],[11,14],[12,0],[12,2],[12,4],[12,6],[12,8],[12,10],[12,12],[12,14],[13,0],[13,14],[14,0],[14,1],[14,2],[14,3],[14,4],[14,5],[14,6],[14,7],[14,8],[14,9],[14,10],[14,11],[14,12],[14,13],[14,14]],"blocks":[[5,6],[11,1],[13,3],[4,13],[1,4],[10,1],[3,13],[11,10],[9,1],[5,7],[13,4],[1,5],[13,11],[9,2],[7,8],[12,3],[3,6],[11,3],[7,9],[8,1],[3,7],[6,9],[9,10],[2,7],[5,8],[11,4],[4,1],[7,2],[9,11],[5,9],[10,5],[8,11],[13,6],[11,12],[1,7],[6,3],[4,9],[9,4],[3,1],[11,13],[7,11],[5,2],[10,13],[1,8],[9,5],[6,11],[9,12],[5,3],[3,9],[8,5],[11,6],[4,3],[7,4],[3,10],[3,3],[9,13],[5,12],[10,7],[8,13],[13,8],[1,10],[7,5],[3,4],[6,5],[2,11],[2,3],[13,9],[1,11],[9,7],[7,13],[12,9],[8,7],[11,9],[5,5],[10,9],[12,11],[10,3],[13,5],[7,3],[7,10],[12,5],[5,11],[11,7],[4,11],[9,6],[3,11],[11,8],[3,12],[1,3],[7,7]],"players":[{"name":"敵","pos":{"x":1,"y":2},"power":2,"setBombLimit":2,"ch":"敵","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":0},{"name":"あなた","pos":{"x":2,"y":13},"power":2,"setBombLimit":2,"ch":"あ","isAlive":true,"setBombCount":1,"totalSetBombCount":1,"id":1},{"name":"何か","pos":{"x":12,"y":1},"power":2,"setBombLimit":2,"ch":"何","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":2},{"name":"何か","pos":{"x":12,"y":13},"power":2,"setBombLimit":2,"ch":"何","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":3}],"bombs":[{"pos":{"x":2,"y":13},"timer":9,"power":2}],"items":[],"fires":[]}
```

![Turn29](https://raw.githubusercontent.com/goto13/CSBombmanServer/image/images/turn29.png "Turn29")

```
{"turn":29,"walls":[[0,0],[0,1],[0,2],[0,3],[0,4],[0,5],[0,6],[0,7],[0,8],[0,9],[0,10],[0,11],[0,12],[0,13],[0,14],[1,0],[1,14],[2,0],[2,2],[2,4],[2,6],[2,8],[2,10],[2,12],[2,14],[3,0],[3,14],[4,0],[4,2],[4,4],[4,6],[4,8],[4,10],[4,12],[4,14],[5,0],[5,14],[6,0],[6,2],[6,4],[6,6],[6,8],[6,10],[6,12],[6,14],[7,0],[7,14],[8,0],[8,2],[8,4],[8,6],[8,8],[8,10],[8,12],[8,14],[9,0],[9,14],[10,0],[10,2],[10,4],[10,6],[10,8],[10,10],[10,12],[10,14],[11,0],[11,14],[12,0],[12,2],[12,4],[12,6],[12,8],[12,10],[12,12],[12,14],[13,0],[13,14],[14,0],[14,1],[14,2],[14,3],[14,4],[14,5],[14,6],[14,7],[14,8],[14,9],[14,10],[14,11],[14,12],[14,13],[14,14]],"blocks":[[5,6],[11,1],[13,3],[4,13],[1,4],[10,1],[11,10],[9,1],[5,7],[13,4],[1,5],[13,11],[9,2],[7,8],[12,3],[3,6],[11,3],[7,9],[8,1],[3,7],[6,9],[9,10],[2,7],[5,8],[11,4],[4,1],[7,2],[9,11],[5,9],[10,5],[8,11],[13,6],[11,12],[1,7],[6,3],[4,9],[9,4],[3,1],[11,13],[7,11],[5,2],[10,13],[1,8],[9,5],[6,11],[9,12],[5,3],[3,9],[8,5],[11,6],[4,3],[7,4],[3,10],[3,3],[9,13],[5,12],[10,7],[8,13],[13,8],[1,10],[7,5],[3,4],[6,5],[2,11],[2,3],[13,9],[1,11],[9,7],[7,13],[12,9],[8,7],[11,9],[5,5],[10,9],[12,11],[10,3],[13,5],[7,3],[7,10],[12,5],[5,11],[11,7],[4,11],[9,6],[3,11],[11,8],[3,12],[1,3],[7,7]],"players":[{"name":"敵","pos":{"x":1,"y":1},"power":2,"setBombLimit":2,"ch":"敵","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":0},{"name":"あなた","pos":{"x":1,"y":12},"power":2,"setBombLimit":2,"ch":"あ","isAlive":true,"setBombCount":0,"totalSetBombCount":1,"id":1},{"name":"何か","pos":{"x":12,"y":1},"power":2,"setBombLimit":2,"ch":"何","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":2},{"name":"何か","pos":{"x":12,"y":13},"power":2,"setBombLimit":2,"ch":"何","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":3}],"bombs":[],"items":[{"pos":{"x":3,"y":13},"name":"力"}],"fires":[[2,13],[1,13],[3,13]]}
```

![Turn30](https://raw.githubusercontent.com/goto13/CSBombmanServer/image/images/turn30.png "Turn30")

```
{"turn":30,"walls":[[0,0],[0,1],[0,2],[0,3],[0,4],[0,5],[0,6],[0,7],[0,8],[0,9],[0,10],[0,11],[0,12],[0,13],[0,14],[1,0],[1,14],[2,0],[2,2],[2,4],[2,6],[2,8],[2,10],[2,12],[2,14],[3,0],[3,14],[4,0],[4,2],[4,4],[4,6],[4,8],[4,10],[4,12],[4,14],[5,0],[5,14],[6,0],[6,2],[6,4],[6,6],[6,8],[6,10],[6,12],[6,14],[7,0],[7,14],[8,0],[8,2],[8,4],[8,6],[8,8],[8,10],[8,12],[8,14],[9,0],[9,14],[10,0],[10,2],[10,4],[10,6],[10,8],[10,10],[10,12],[10,14],[11,0],[11,14],[12,0],[12,2],[12,4],[12,6],[12,8],[12,10],[12,12],[12,14],[13,0],[13,14],[14,0],[14,1],[14,2],[14,3],[14,4],[14,5],[14,6],[14,7],[14,8],[14,9],[14,10],[14,11],[14,12],[14,13],[14,14]],"blocks":[[5,6],[11,1],[13,3],[4,13],[1,4],[10,1],[11,10],[9,1],[5,7],[13,4],[1,5],[13,11],[9,2],[7,8],[12,3],[3,6],[11,3],[7,9],[8,1],[3,7],[6,9],[9,10],[2,7],[5,8],[11,4],[4,1],[7,2],[9,11],[5,9],[10,5],[8,11],[13,6],[11,12],[1,7],[6,3],[4,9],[9,4],[3,1],[11,13],[7,11],[5,2],[10,13],[1,8],[9,5],[6,11],[9,12],[5,3],[3,9],[8,5],[11,6],[4,3],[7,4],[3,10],[3,3],[9,13],[5,12],[10,7],[8,13],[13,8],[1,10],[7,5],[3,4],[6,5],[2,11],[2,3],[13,9],[1,11],[9,7],[7,13],[12,9],[8,7],[11,9],[5,5],[10,9],[12,11],[10,3],[13,5],[7,3],[7,10],[12,5],[5,11],[11,7],[4,11],[9,6],[3,11],[11,8],[3,12],[1,3],[7,7]],"players":[{"name":"敵","pos":{"x":1,"y":1},"power":2,"setBombLimit":2,"ch":"敵","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":0},{"name":"あなた","pos":{"x":1,"y":12},"power":2,"setBombLimit":2,"ch":"あ","isAlive":true,"setBombCount":0,"totalSetBombCount":1,"id":1},{"name":"何か","pos":{"x":12,"y":1},"power":2,"setBombLimit":2,"ch":"何","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":2},{"name":"何か","pos":{"x":12,"y":13},"power":2,"setBombLimit":2,"ch":"何","isAlive":true,"setBombCount":0,"totalSetBombCount":0,"id":3}],"bombs":[],"items":[{"pos":{"x":3,"y":13},"name":"力"}],"fires":[]}
```



### Client sends string value.

- At first, client sends his name. ex. "テスト"
- Secondary and continuously, client sends his action. ex. "UP, false".
- Action value has two parts separated by comma.
  - 1st part is direction to move. You can use either one of UP, DOWN, RIGHT, LEFT. if you want to stay at the same place, please insert something other than UP, DOWN, RIGHT, LEFT.
  - 2nd part is whether you set bomb at the place.
- You can send "UP, true". In this case, if you can move to the upper side of you, put the bomb in the place you are at and move to the upper side. The bomb is not placed on the moving destination, but is placed at the source location.

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
			Console.WriteLine("何か", Console.OutputEncoding.CodePage);
			//標準入力
			string[] moves = { "UP", "DOWN", "LEFT", "RIGHT" };
			var rand = new Random();
			while (true)
			{
				var s = Console.ReadLine();
				Console.WriteLine(moves[rand.Next(0, 3)] + ",false");
			}
		}
	}
}
```


