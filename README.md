# 📚 Hiscaries Library of Stories
Welcome to the **Hiscary Library of Stories** repository! This project (referred to as *application*) is undergoing a complete refactor, following the best practices known to date. 🛠️

## 🚀 Refactoring Plan for the Server
The following steps outline the server refactoring process:

1. **Domain Layer**  
   - Refactor domain entities to include domain-specific methods.
  
2. **API Controllers**  
   - Ensure API controllers handle only Mediator logic and nothing else.
  
3. **Entity Framework Core (EF Core) Configuration**  
   - Add entity configurations for the persistence layer.
  
4. **Mediator Commands & Queries**  
   - Refactor Mediator commands and queries to properly use service interfaces.
  
5. **DTO Optimization**  
   - Simplify Read DTOs, ensuring they only carry necessary data.
  
6. **Application Layer**  
   - Split the application layer into *Read* and *Write*:  
     - **Read**: Utilize Dapper with Read Models (DB Views).  
     - **Write**: Use EF Core with Change Tracking and strict transactional boundaries within one aggregate root.
  
7. **Service Layer Implementation**  
   - Implement the application’s Read and Write services for all interface contracts.
  
8. **REST API Best Practices**  
   - Refactor API to adhere to the latest REST best practices.  
   - Implement security roles to restrict access to authorized users for specific actions.

9. **Persistence Layer**  
   - Refactor persistence logic to use EF Core for writing and Dapper for reading.

10. **SQL Views & Migrations**  
    - Add view SQL generation as part of EF Core migrations.

11. **Global Exception Handling**  
    - Implement global exception handling.

12. **Logging**  
    - Add comprehensive system logging.

13. **Authorization & Role Checks**  
    - Ensure proper authorization so that users can only delete their own content, while admins have broader rights (e.g., delete all stories or comments).

14. **Docker**  
    - Add Docker to encapsulate the entire system for easy deployment.

15. **Optimistic Concurrency**  
    - Implement optimistic concurrency control.

16. **Audit Fields**  
    - Add audit fields (e.g., `CreatedAt`, `UpdatedAt`) for all entities.

17. **Image Storage**  
    - For image uploads (e.g., story preview images):  
      - Store images locally or on the cloud, not in the DB as blobs.  
      - Return URLs for image access when delivering to the client.

18. **Pagination**  
    - Add pagination for large datasets (e.g., search results).

19. **Caching**  
    - Implement caching at all levels: frontend, HTTP, API, Redis, DB, etc.

20. **TODO Fixes**  
    - Address all existing TODOs.

21. **Dev Testing**  
    - Perform thorough testing to ensure the system is robust, especially in scenarios involving high interaction (e.g., adding content while editing stories or performing repeated actions).

22. **Unit & Integration Tests**  
    - Add unit tests and integration tests for full coverage.

23. **Specification Pattern**  
    - Introduce the specification pattern to repositories.

24. **Domain Events**  
    - Implement domain events.

25. **JWT Service**  
    - Extract JWT token generation into a separate service or utilize an identity provider.

## 🌟 Future Improvements & Features
1. **AI/ML Integration**  
   - Create a separate microservice that uses AI/ML to generate stories.  
   - Simulate user activity with AI to enhance logging, metrics, and system performance insights.

2. **User-Uploaded PDFs**  
   - Allow users to upload their own PDF files (pending admin approval due to copyright).

3. **Private Stories**  
   - Add an option for private stories that can only be shared via one-time links or similar mechanisms.

---

## 🎨 Refactoring Plan for the Client
1. **Component Separation**  
   - Split the frontend into reusable components.

2. **State Management**  
   - Integrate NgRx for state management. Learn more [here](https://ngrx.io/guide/store).

3. **Mobile Support**  
   - Ensure the app is mobile-responsive.

---

## 🛠️ Configuration Settings
Configure your settings in your chosen config file as follows:

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

In your `.env` file (to be located next to `docker-compose.yml`), include the following:
```
POSTGRES_PASSWORD=***
```

---

🎉 **Thank you for visiting the repository!**  
Feel free to contribute or raise any issues to improve the application!