# Author: Dan Rozenberg
## JobSity Challenge

First you have to run the docker-compose.yml to create containers
`docker-compose up -d`

### Users
There are 3 users seeded with the following users and passwords
| Username | password |
| ------ | ------ |
| goku | Kamehameha |
| krilin |AlwaysDie|
| vegeta | PrinceSSJ |

I don't know how you usually run docker but maybe you need to add the entry `127.0.0.1 kafka` in your hosts file.
The hosts file is in the following root `C:\Windows\System32\drivers\etc`

To run the chat app you need to execute 2 bats
- runServer.bat
- runBot.bat

After the backend is running, you can open the `front/index.html`
