namespace Rias.App

module Storage =
    open Rias.Common
    open Rias.Persistence

    let map storage toDto toEvent =

        let asEvents =
            Seq.map toEvent 
            >> Result.leave
            >> Result.map Seq.cast

        let mapped = 
            {
                load = storage.load >> Result.bind asEvents
                store = Seq.map toDto >> storage.store
            }
        mapped


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

        let events = WriteSideProcessing.handleCommand 
                        BookRoot.aggregate 
                        bookStorage 
                        openCmd

        events
