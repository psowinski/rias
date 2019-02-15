namespace Rias.Domain

open System.Transactions
module BookDto =
    open Rias.Contract.Domain
    open System
    open BookRoot

    type EventBoxDto<'event> = {
        streamId: string
        version: int
        event: 'event        
    }

    let toDtoEvent dataToDto (event : EventBox<'event>) : EventBoxDto<'eventdto> =
        {
            streamId = StreamId.toString(event.StreamId)
            version = event.Version
            event = dataToDto event.Event
        }

    type BookOpenedDto = {
        name: string
        date: string
    }

    type TransactionAccountedDto = {
        ordinalNumber: int
        accountingDate: string
        transactionId: string
    }

    type BookEventDto = {
        name: string
        bookOpened: BookEventDto option
        transactionAccounted: TransactionAccountedDto option
    }

    let bookOpenedToDto (data: Args.OpenNewBookArgs) =
        {
            name = data.Name
            date = data.Date.ToShortDateString()
        }

    let transactionAccountedToDto (data: Transaction) =
        {
            ordinalNumber = data.OrdinalNumber |> fun (OrdinalNumber v) -> v
            accountingDate = data.AccountingDate.ToShortDateString() 
            transactionId = StreamId.toString data.TransactionId
        }

    let bookEventToDto (event : Event) =
        match event with
        | BookRoot.BookOpened data -> { name = "BookRoot.OpenNewBook"
                                        bookOpened = None
                                        transactionAccounted = None
                                      }
        | _ -> failwith ""


module TestJSON =
    open Fable.SimpleJson
    open System
    let data () = 
        [ "name", JString "ala"
          "age", JNumber 21.0 ]
        |> Map.ofList
        |> JObject
