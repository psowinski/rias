namespace Rias.Persistence

[<AutoOpen>]
module StorageContract =
    open Rias.Common

    type Storage<'T> = {
        load: string -> Async<Result<seq<'T>, string>>
        store: seq<'T> -> Async<Result<unit, string>>
    }
