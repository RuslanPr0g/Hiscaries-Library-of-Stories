# Hiscaries Library of Stories
This repository is dedicated to one of my projects. This project (further referenced as application) will be refactored using every best practice known to mankind.

### Refactoring Plan for the Server
The following steps are outlined for refactoring the server:

* Refactor domain to include domain methods
* Refactor API controllers to use only mediator LOGIC
* Add entity configuration for persistence layer EF core
* Refactor mediator commands and queries to use service interfaces properly
* Simplify read dto as much as possible, it carries too much info
* Refactor application layer, separate it into read and write, where read would be dapper with read models (views in the db), and write as EF CORE with change tracker and strict transactional bound within one aggregate root.
* Implement the application read and write layers for all the interface contacts
* Refactor API level to use all the best practices regarding REST, etc. (all the rest practices)
* also how to secure api in terms of roles, etc. so that only authorized user can perform certain action
* Refactor persistence level to use ef core for write and dapper for read logic
* Add view sql generation into the migrations feature of ef core
* Add global exception handling
* Add logging for the system
* Add auth checks for user roles, etc, such as user should be able to delete only its own comment, story, etc. only admin can delete all ,etc.
* Add docker to have it all in one place
* Add optimistic concurrency
* Add audit (created at, updated at, etc) for Entity<>
* Populate this readme with how to run information
* Add unit tests
* Add wrapper around datetime, etc.
* Add integration tests
* Add specification pattern to the repositories
* Add domain events, etc.
* Extract the jwt token generation to a separate service or identity provider
* when uploading any image (such as story preview image), do not save the whole file blob to DB, save the file locally or on the cloud
* ALSO when you need to show the image to a client, send a url to access this image on the cloud
* Add pagination to search page or smth
* Add cache at all levels, front end, http, api, redis, db, etc.
* Fix TODOs
* Dev test everything trying to break things, when adding contents while modifying story, or click a lot of times, etc.

### In the Future
Create a separate module/system/microservice that will be using an ML/AI to generate stories, not only that, but also
AI will simulate user activity in the entire system, such that we can improve upon logging, metrics, etc.

### Refactoring Plan for the Client
The following steps are outlined for refactoring the client:

* Separate into different components
* Add store https://ngrx.io/guide/store
* add mobile view support (mobile responsiveness)

### Configuration Settings
Please, also set keys in your preferred config file as follows:
```json
{
  "Kestrel:Certificates:Development:Password": "***",
  "JwtSettings:Key": "***",
  "JwtSettings:Issuer": "***",
  "JwtSettings:Audience": "***",
  "SaltSettings:StoredSalt": "***",
  "ConnectionStrings:PostgresEF": "Server=postgresdb;Port=5432;User Id=postgres;Password=***;Database=hiscarydbef;Include Error Detail=true;"
}
```
.env file should contain the following (the file itself should be next to the docker-compose.yml file):
```
POSTGRES_PASSWORD=***
```
