namespace Fable.Import.Azure

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.JS

module AzureCosmos =
    [<AbstractClass>]
    type CosmosMain =
        [<Emit("new $0($1...)")>] abstract CreateClient: options: CosmosClientOptions -> CosmosClient

    and CosmosClient =
        abstract databases: Databases with get

    and Databases =
        abstract createIfNotExists: unit -> unit

    and CosmosClientOptions =
        abstract endpoint: string with get, set
        abstract key: string with get, set

    [<Emit("new CosmosClient($0)")>]
    let createClient (options : CosmosClientOptions) : CosmosClient = jsNative

[<AutoOpen>]
module cosmos_Extension =
    let [<Import("CosmosClient", "@azure/cosmos")>] cosmos: AzureCosmos.CosmosMain = failwith "JS only" 

module cosmos =
    let ala () = ()

module xxx =
    cosmos.ala