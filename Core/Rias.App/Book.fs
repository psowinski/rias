namespace Rias.App

module Book =
    open Rias.Common
    open Rias.Contract.Domain
    open Rias.Domain
    open Rias.Persistence
    open System
    
    let OpenNewBook () =

        let bookStorage = InMemory.create<BookRoot.Event> ()

        let openCmd = { StreamId = (StreamId.generate "book") |> Result.okValue
                        Command =  BookRoot.Command.OpenNewBook { Name = "my book"; Date = DateTime.Parse("2018-10-10")}}

        let events = WriteSideProcessing.handleCommand 
                        BookRoot.aggregate 
                        bookStorage 
                        openCmd

        ()
