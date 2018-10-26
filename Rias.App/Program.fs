open Rias.Contract.Domain
open Rias.Domain
open Rias.Persistence
open Rias
open Rias.App
open Rias.Common
open System

[<EntryPoint>]
let main argv =

    let bookStorage = InMemory.create<BookRoot.Event> ()
    let openCmd = { StreamId = (StreamId.generate "book") |> Result.okValue
                    Command =  BookRoot.Command.OpenNewBook { Name = "my book"; Date = DateTime.Parse("2018-10-10")}}

    let events = App.handleCommand 
                    BookRoot.aggregate 
                    bookStorage 
                    openCmd

    0
