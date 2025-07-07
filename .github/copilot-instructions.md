orgRules:
  priority: P0
  rules:
    - id: org-language-discipline
      description: Only write code in the language(s) already present in the repo. No new languages.

    - id: org-lint-format
      description: Enforce ESLint and Prettier across all JS/TS repos; run auto-fix on save and in CI.

    - id: org-commits-versioning
      description: 
        - Use Conventional Commits (feat:, fix:, chore:, etc.). 
        - Follow SemVer MAJOR.MINOR.PATCH.
        - Generate annotated Git tags for each release. 
        - Copilot should suggest bump commits and tag commands in PR reviews.

    - id: org-security-first
      description: 
        - Validate inputs; use parameterized queries. 
        - Sanitize outputs. 
        - Remind of OWASP Top 10 vulnerabilities when relevant (XSS, SQLi, CSRF, etc.).
        - Implement proper error handling.

    - id: org-testing-suggestions
      description: Always propose relevant test cases alongside delivered functionality. Frameworks and thresholds are repo-specific.

    - id: org-file-creation
      description: Only create new files when explicitly requested or absolutely required (e.g. missing CI/pipeline config). Otherwise, modify existing files.

    - id: org-dependency-check
      description: On adding dependencies, remind to run `npm audit` or equivalent SCA scan.

    - id: org-naming-conventions
      description:
        - camelCase for variables and functions
        - PascalCase for classes, components, types
        - kebab-case for filenames


csharpRules:
  priority: P1
  overrides: orgRules
  rules:
    - id: cs-language-orm
      description: Use C# (repo’s .NET version) with Entity Framework. No ad-hoc SQL files unless explicitly requested.

    - id: cs-architecture-layering
      description: 
        - Keep controllers/services under 5 endpoints per file.
        - Enforce domain/service separation.

    - id: cs-security-logging
      description: 
        - Sanitize inputs, enable CSRF protection, apply JWT/OAuth patterns, enforce least-privilege.
        - Snark only in dev-only logs; production uses structured logging.

    - id: cs-testing
      description:
        - Frameworks: xUnit + Moq for unit tests, plus integration tests.
        - Target: ≥70% coverage.

    - id: cs-doc-style
      description:
        - XML comments on public APIs.
        - Enforce `.editorconfig` or StyleCop rules.

    - id: cs-versioning
      description: Copilot suggests bump commits (`chore(release): next patch`) and Git tag commands in PRs.

    - id: cs-dependency-check
      description: Remind to run `dotnet list package --vulnerable` or your SCA tool after adding packages.
