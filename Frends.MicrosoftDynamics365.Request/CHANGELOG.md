# Changelog

## [1.2.0] - 2026-04-22
### Added
- New `EnvironmentType` input parameter (`Dataverse` / `FinanceAndOperations`) to support both D365 platform stacks.
- Finance & Operations support: uses `/data/{path}` URL pattern instead of the Dataverse `/api/data/{version}/{path}` pattern.

### Changed
- URL construction is now branched based on the selected environment type.
- Default `EnvironmentType` is `Dataverse`, maintaining backward compatibility with existing processes.

## [1.1.0] - 2024-07-08
### Changed
- Added support for PATCH requests and adjusted how the task handles responses from the Dynamics365 API if they have no content.


## [1.0.0] - 2024-06-10
### Changed
- Initial implementation.
