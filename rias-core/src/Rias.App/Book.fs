namespace Rias.App

module Book =
    open Rias.Common
    open Rias.Domain
    open Rias.Persistence
    open System
    open Fable.Core

    let OpenNewBook () = promise {
        let! storage = Cosmos.connect "rias"
        let bookStorage = Storage.map storage BookDto.api.bookEventToDto (fun _ -> Ok Dto.Null)

        let openCmd = { StreamId = (StreamId.generate "book") |> Result.okValue
                        Command =  BookRoot.Command.OpenNewBook { Name = "my book"; Date = DateTime.Parse("2018-10-10")}}
        return "ok"
        // let! result = Processing.handleCommand
        //                 BookRoot.aggregate
        //                 bookStorage
        //                 openCmd

        // return result
    }
