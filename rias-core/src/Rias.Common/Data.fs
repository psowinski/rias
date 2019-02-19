namespace Rias.Common

[<AutoOpen>]
module Data =

    type Dto =
        | Number of float
        | String of string
        | Bool of bool
        | List of List<Dto>
        | Map of Map<string, Dto>
        | Null