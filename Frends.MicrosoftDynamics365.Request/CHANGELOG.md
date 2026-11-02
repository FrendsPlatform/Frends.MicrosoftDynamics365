# Changelog

## [2.0.0] - 2026-11-02
### Changed
- **[Breaking Change]** Requests now throw on failure by default; set `ThrowErrorOnFailure` to `false` to return a result with error details.
- Added structured error details and configurable failure messages for Dynamics 365 requests.

## [1.1.0] - 2024-07-08
### Changed
- Added support for PATCH requests and adjusted how the task handles responses from the Dynamics365 API if they have no content.


## [1.0.0] - 2024-06-10
### Changed
- Initial implementation.
