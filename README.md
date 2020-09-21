# Neos.Leaderboard
This service provides methods to handle quests and games used within NEOS VR (https://neos.com)

# Questmanager
Provides methods to create, modify and delete quests and quest steps for your NEOS-Account.
Also handles quest step progression for NEOS users and a leader board per quest.

## Controller methods
Route | Action | Description
------------ | ------------- | -------------
../api/accounts | POST | Gets the account for the given LoginDto. Creates a new account if the account doesn't exist
../api/accounts/[Guid]/quests | GET | Gets all quest for the given account
../api/quests/[Guid] | GET | Gets the quest with the given key
../api/quests | POST | Adds or updates the given quest.
../api/quests/[Guid]/leaderboard | GET | Gets the leader board for the given quest
../api/quests/[Guid]/queststeps | GET | Gets the quest steps for the given quest
../api/quests[Guid]/nextqueststep?username=[Username] | GET | Gets the next quest step for the given user
../api/quests/[Guid] | DELETE | Deletes the given quest. Quest steps and quest step progression will be deleted too. Not yet implemented.
../api/queststeps/[Guid] | GET | Gets the quest step for the given key
../api/queststeps | POST | Adds or updates the given quest steps
../api/queststeps | DELETE | Deletes the quest step with the given key. Not yet implemented.
../api/queststeps/[Guid]/movedown | POST | Moves the given quest step to the previous position and changes the previous quest step. Do not move quest steps when CanReorder is false
../api/queststeps/[Guid]/moveup | POST | Moves the given quest step to the next position and changes the next quest step. Do not move quest steps when CanReorder is false
../api/queststepprogressions | POST | Adds a new quest step progression for the given dto

# Gamemanager (tbd)
Provides methods to create, modify and delete games for your NEOS-Account. 
Also handles adding hight socres to games and a leader board per quest.
