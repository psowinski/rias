namespace Rias.Persistence

module Cosmos =
    open Rias.Common
    open Fable.Core
    open Fable.Import.JS

    type private NativeStorage = {
        load: string -> Promise<Result<seq<obj>, string>>
        store: seq<obj> -> Promise<Result<unit, string>>
    }

    [<Emit("connect($0)")>]
    let private connectNative (dbName: string) : Promise<NativeStorage> = jsNative

    let connect dbName = async {
        let! cosmos = connectNative dbName |> Async.AwaitPromise

        let storage : Storage<Dto> = {
            load = cosmos.load >> Async.AwaitPromise >> Result.mapAsync (Seq.map Dto.toDto)
            store = Seq.map Dto.toJS >> cosmos.store >> Async.AwaitPromise
        }
        return storage
    }
