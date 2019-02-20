namespace Rias.App

module Book =
    open Rias.Common
    open Rias.Domain
    open Rias.Persistence
    open System
    
    let OpenNewBook () =

        let storage = InMemory.create ()
        let bookStorage = Storage.map storage BookDto.api.bookEventToDto (fun _ -> Ok Dto.Null)

        let openCmd = { StreamId = (StreamId.generate "book") |> Result.okValue
                        Command =  BookRoot.Command.OpenNewBook { Name = "my book"; Date = DateTime.Parse("2018-10-10")}}

        let events = Processing.handleCommand 
                        BookRoot.aggregate 
                        bookStorage 
                        openCmd

        events
