# Mario #


### 1. How to Play ###

 * Press cursor keys to move Player.

 * Go to "NEXT" to go to next stage.


	|            |
	|   O       N|
	|           E|
	|     =|    X|
	| =    |    T|
	|      |     |
	|      |    ||
	|   =  |    V|
	| =    |   | |
	AAAAAAA|AAA| |

------------------------------------------------------------
### 2. How to make ###

#### Compiling ####
	csc Mario.cs -win32icon:icon.ico
You must write map yourself if you want to change map.

Each character has different mean.

	"A" "V" ">" "<"	----->    thorn (">" and "<" which are written in movemap have different means : moving left or moving right.)

	"="	-----> trampoline

	

You can choose writing place,source code or data file.

You write "Data" if you write data file.

Data's format is xml format.
(It use xml Serialization)

* Example
```xml
<?xml version="1.0" encoding="utf-8"?>
	<ArrayOfMario>
		<Mario>
			<map>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>| m                     |</string>
				<string>|-------           =    |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|AAAAAAAAAAAAAAAAAAAAAAA|</string>
			</map>
			<hidemap>
				<string>|                       |</string>
				<string>|                    O  |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>| >                     |</string>
				<string>|-------     =     =    |</string>
				<string>|            -     -    |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|AAAAAAAAAAAAAAAAAAAAAAA|</string>
			</hidemap>
			<movemap>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>| >                     |</string>
				<string>|--------               |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|                       |</string>
				<string>|-----------------------|</string>
			</movemap>
	</Mario>
</ArrayOfMario>
```
---
### 3. System Requirements ###

 *  Windows7
  * I checked runing in Windows 7 and Windows 10
 *  .NET Framework version 2.0 / C# 2.0
