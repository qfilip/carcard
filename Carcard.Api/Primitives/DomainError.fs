namespace Carcard.Api.Primitives

type DomainError =
| Validation of string list
| Rejected of string

