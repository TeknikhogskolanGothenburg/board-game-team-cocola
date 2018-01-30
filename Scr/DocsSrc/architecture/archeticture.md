# Board game architecture

* How are your board game build?
* Which components does your application consist of?

* We have created a GameEngine that holds our functions in separate classes: DataBase, Player, StringController and Game.

* In our DataBase class we have functions such as CreateGameKey that generates a random key from a set of letters,
Exists that checks if a key already is exists in sql database, GetPlayers that gets the information
about existing players, GetGameStatus that returns status 
of the game from database, ChangeGameStatus that changes the status of a game in database and InsertToDataBase 
inserts the key and a players nickname into database.

* In our Player class we've created properties such as Nickname, IsAdmin, Points, Attempt. I Game class we use 
Player class to create a List of Players and other properies such as enum _IsAdmin {Yes, NO}, GameStatus and others.
We have a Game propery that checks if a game exists and if so attaches the game to a gamekey. GetCurrentPlayer that attaches 
the players to a game. InsertPlayer that returns a boolean to our IsAdmin propery and checks if the game is existing in
our database. ChangeGameStatus that checks if the game has started or not. ReadGamePlayers and ReadGameStatus that gets info
about players and gamestatus from our database. We've also a StringController class that checks if a string is empty.

* We have one controller HomeController with several actions, that uses our Database class functions to accomplish 
the needed results in our Views. Some of the controllers, such as GameCreator, GameLogin and GameLobby have functions that 
redirects us to our visible views. We call them "invisible views". Our GameCreator holds cookie and GameLobby controller 
session. Cookie saves the gamekey and players nickname. Session checks the information in cookie so if the info is
the same the game can start.

* We have 5 views. The starter view is Index, where the player chooses between creating a new game or join existing game. 
Depending in what the player chooses the GameCreator controller redirects them to JoinGame view (via the GameLobby controller) or NewGame view.
The GameLogin controller controlls if the player is entering anything in our checkboxes in view. If not the controller redirects
player to the startpage Index view. GameLobby checks if a player exists in our database and redirects player to the RunGame page and if 
player doesn't exsist - back to the startpage Index.

