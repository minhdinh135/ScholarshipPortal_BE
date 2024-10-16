# SSAP ASP.NET Core Web API

This is the repository that provides APIs for developing SSAP system.

## Project Structure

```
├── src
│   ├── Core                    # Contains the core business logic and domain models, view models, etc.
│   ├── Infrastructure          # Contains infrastructure concerns such as data access, external services, etc.
│   └── API                      # Contains the API layer, including controllers, extensions, etc.
├── tests
│   ├── Core.Tests              # Contains unit tests for the core layer
│   ├── Infrastructure.Tests    # Contains unit tests for the infrastructure layer
│   └── API.Tests                # Contains unit tests for the API layer
└── README.md                   # Project documentation (you are here!)
```

## Usage

First, navigate to `API` directory by running the following command:

```bash
cd ./API
```

Most of the time this is the only command you'll run:

```bash
dotnet run
```

To launch with a specific profile, run the following command:

```bash
dotnet run --launch-profile {profile_name}
```

### Reference

[Project Structure](https://binarybytez.com/understanding-clean-architecture/)
