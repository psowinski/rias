namespace Rias.Persistence

module Cosmos =
    open Rias.Common
    open Fable.Import.JS
    open Fable.Core.JsInterop

    let private connectNative: string -> Promise<Storage<obj>> = import "connect" "cosmos-es-driver"

    let connect dbName = promise {
        let! cosmos = connectNative dbName

        let storage : Storage<Dto> = {
            load = cosmos.load >> Result.promiseMap (Seq.map Dto.toDto)
            store = Seq.map Dto.toJS >> cosmos.store
        }
        return storage
    }
