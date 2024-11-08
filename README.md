# 📚 Hiscaries Library of Stories

Welcome to the **Hiscary Library of Stories** repository! This project (referred to as _application_) is undergoing a complete refactor, following the best practices known to date. 🛠️

## 🚀 Refactoring Plan for the Server 🚀

The following steps outline the server refactoring process:

0. **Performance**

   - Take a look at different places and see where we need to improve performance
   - For example, recommendations endpoint gives us all the stories, without any limitations

1. **Improve Identity Provider**

   - Move identity providing (jwt, oauth, etc.) to a separate service (OpenIddict as a separate AUTH server with its separate DB, etc.).

2. **Domain Layer**

   - Refactor domain entities to include domain-specific methods.
   - Add domain services

3. **Deployment**

   - Streamline the deployment process to allow for one-click application deployment.

4. **Auth and Security**

   - Add roles to the system and also validation that user is authorized to perform certain actions.

5. **DTO Optimization** 🚀

   - Simplify Read DTOs, ensuring they only carry necessary data.

6. **Application Layer** 🚀

   - Split the application layer into _Read_ and _Write_:
   - **Read**: Utilize Dapper with Read Models (DB Views).
   - **Write**: Use EF Core with Change Tracking and strict transactional boundaries within one aggregate root.

7. **Pagination Implementation**

   - Implement pagination when fetching data

8. **REST API Best Practices** 🚀

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

17. **Pagination**

    - Add pagination for large datasets (e.g., search results).

18. **Caching**

    - Implement caching at all levels: frontend, HTTP, API, Redis, DB, etc.

19. **TODO Fixes**

    - Address all existing TODOs.

20. **Dev Testing**

    - Perform thorough testing to ensure the system is robust, especially in scenarios involving high interaction (e.g., adding content while editing stories or performing repeated actions).

21. **Unit & Integration Tests**

    - Add unit tests and integration tests for full coverage.

22. **Specification Pattern**

    - Introduce the specification pattern to repositories.

23. **Domain Events**

    - Implement domain events.

24. **Other microservices**
    - Think about what other microservice to add that would leverage event sourcing. But it should be reasonable in terms of my app.

## 🌟 Future Improvements & Features

1. **AI/ML Integration**

   - Create a separate microservice that uses AI/ML to generate stories.
   - Simulate user activity with AI to enhance logging, metrics, and system performance insights.

2. **User-Uploaded PDFs**

   - Allow users to upload their own PDF files (pending admin approval due to copyright).

3. **Private Stories**
   - Add an option for private stories that can only be shared via one-time links or similar mechanisms.

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
