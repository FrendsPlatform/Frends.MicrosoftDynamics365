# Changelog

## [2.0.0] - 2026-11-02
### Changed
- **[Breaking Change]** On the Options tab, `ThrowExceptionOnErrorResponse` was replaced by `ThrowErrorOnFailure`, which defaults to `true`. Set it to `false` to return a result instead of throwing when a request fails.
- The result now includes an `Error` object with `Message` and `AdditionalInfo` properties; `ErrorMessageOnFailure` on the Options tab lets you override the failure message.
- The task now targets .NET 8 (previously .NET 6).

## [1.1.0] - 2024-07-08
### Changed
- Added support for PATCH requests and adjusted how the task handles responses from the Dynamics365 API if they have no content.


## [1.0.0] - 2024-06-10
### Changed
- Initial implementation.
