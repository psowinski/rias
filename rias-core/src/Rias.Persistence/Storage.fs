namespace Rias.Persistence

[<AutoOpen>]
module StorageContract =
    open Rias.Common
    open Fable.Import.JS

    type Storage<'T> = {
        load: string -> Promise<Result<seq<'T>, string>>
        store: seq<'T> -> Promise<Result<unit, string>>
    }
