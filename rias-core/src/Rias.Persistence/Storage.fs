namespace Rias.Persistence

[<AutoOpen>]
module StorageContract =
    open Rias.Common

    // type Storage<'T> = {
    //     load: string -> Result<seq<'T>, string>
    //     store: seq<'T> -> Result<unit, string>
    // }

    type Storage<'T> = {
        load: string -> Async<Result<seq<'T>, string>>
        store: seq<'T> -> Async<Result<unit, string>>
    }
