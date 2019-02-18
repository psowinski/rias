namespace Rias.Domain

module BookRoot =
    open System
    open Rias.Common
    open Rias.Contract.Domain

    type OrdinalNumber = private OrdinalNumber of int
    type Transaction = { OrdinalNumber: OrdinalNumber; AccountingDate: DateTime; TransactionId: StreamId }

    module OrdinalNumber =
        let value (OrdinalNumber x) = x
        let first () = OrdinalNumber 1
        let next (OrdinalNumber x) = x + 1 |> OrdinalNumber

    module Args =
        type OpenNewBookArgs = { Name: string; Date: DateTime; }
        type AccountTransactionArgs = { AccountingDate: DateTime; TransactionId: StreamId }

    type Book = {
        Name: string
        OpenDate: DateTime option
        Transactions: Transaction list }

    type Command =
        | OpenNewBook of Args.OpenNewBookArgs
        | AccountTransaction of Args.AccountTransactionArgs

    type Event =
        | BookOpened of Args.OpenNewBookArgs
        | TransactionAccounted of Transaction

    let getEventName event =
        match event with
        | BookOpened _ -> "BookOpened"
        | TransactionAccounted _ -> "TransactionAccounted"

    let nextOrdinalNumber state = 
        match state.Transactions with
        | [] -> OrdinalNumber.first ()
        | x::_ -> OrdinalNumber.next x.OrdinalNumber

    let validateIsTransactionAfterOpenDate state transactionDate =
        match state.OpenDate with
        | Some date -> if transactionDate >= date then Ok ()
                       else Error "Transaction acounted before open date."
        | None -> Error "Book have to be open to account transaction."

    let zero = { Name = ""; OpenDate = None; Transactions = [] }

    let execute box validateZero state (command: CommandBox<Command>) =
        match command.Command with 
        | OpenNewBook args -> state |> validateZero
                                    |> Result.map (fun _ -> BookOpened args)

        | AccountTransaction args -> validateIsTransactionAfterOpenDate state.State args.AccountingDate
                                     |> Result.map (fun _ -> TransactionAccounted { 
                                                        OrdinalNumber = nextOrdinalNumber state.State
                                                        AccountingDate = args.AccountingDate
                                                        TransactionId = args.TransactionId })
        |> Result.map (box state command)

    let apply state event =
        match event.Event with
        | BookOpened args -> { state with Name = args.Name
                                          OpenDate = Some args.Date }
        | TransactionAccounted args -> { state with Transactions = args::state.Transactions }
        |> Ok

    let aggregate = Aggregate.create zero (execute EventBox.boxOne StateBox.validateZero) apply
