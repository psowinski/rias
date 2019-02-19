namespace Rias.Domain

module AccountingTransactionRoot =
    open System

    type AccountSymbol = AccountSymbol of string
    type AccountSide = 
        | Debet 
        | Credit
    type Operation = { Symbol: AccountSymbol; Side: AccountSide; Amount: decimal }
    type BookId = StreamId

    module Args =
        type OpenTransactionArgs = 
            { BookId: BookId 
              RegistrationNumber: string
              DocumentNumber: string
              DocumentDate: DateTime }

    type AccountingTransaction = 
        { BookId: BookId option
          RegistrationNumber: string
          DocumentNumber: string
          DocumentDate: DateTime
          Operations: Operation list
          Frozen: bool }
        
    type Command =
        | OpenTransaction of Args.OpenTransactionArgs
        | AddOperation of Operation
        | CloseTransaction

    type Event =
        | TransactionOpened of Args.OpenTransactionArgs
        | OperationAdded of Operation
        | TransactionClosed of BookId

    let zero = { BookId = None
                 RegistrationNumber = ""
                 DocumentNumber = ""
                 DocumentDate = DateTime.MinValue
                 Operations = []
                 Frozen = false }

    let isTransactionOpen (state: StateBox<AccountingTransaction>) =
        state.Version > 0 && not state.State.Frozen

    let areOperationsComplete operations = 
        let sum predicate = 
            operations |> List.where (fun x -> predicate x.Side)
                       |> List.sumBy (fun x -> x.Amount)

        let dt = sum (function | Debet -> true | _ -> false)
        let ct = sum (function | Credit -> true | _ -> false)

        dt = ct

    let validateOpenTransaction state = if isTransactionOpen state then Ok state
                                        else Error "Transaction is not open"

    let validateOperationsCompletnes state = if areOperationsComplete state.State.Operations then Ok state
                                             else Error "Transaction sides not equal"

    let validateCloseTransaction = validateOpenTransaction >> Result.bind validateOperationsCompletnes

    let execute box validateZero state command =
        match command.Command with
        | OpenTransaction args -> state |> validateZero
                                        |> Result.map (fun _ -> TransactionOpened args)

        | AddOperation args ->  state |> validateOpenTransaction
                                      |> Result.map (fun _ -> OperationAdded args) 

        | CloseTransaction -> state |> validateCloseTransaction
                                    |> Result.map (fun _ -> TransactionClosed state.State.BookId.Value) 
        |> Result.map (box state command)

    let apply state event =
        match event.Event with
        | TransactionOpened args -> { state with BookId = Some args.BookId
                                                 RegistrationNumber = args.RegistrationNumber
                                                 DocumentNumber = args.DocumentNumber
                                                 DocumentDate = args.DocumentDate }
        | OperationAdded args -> { state with Operations = args::state.Operations }
        | TransactionClosed _ -> { state with Frozen = true }
        |> Ok

    let aggregate = Aggregate.create zero (execute EventBox.boxOne StateBox.validateZero) apply
