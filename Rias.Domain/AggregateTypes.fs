namespace Rias.Domain

[<AutoOpen>]
module AggregateContract =

    type AggreagetName = string
    type AggreagetUniqueId = string
    type StreamId = private StreamId of AggreagetName * AggreagetUniqueId

    module StreamId =
        open System
        let create name id = 
            if String.IsNullOrWhiteSpace name then Error "Invalid name."
            elif String.IsNullOrWhiteSpace id then Error "Invalid id."
            else StreamId (name, id) |> Ok

        let toString (StreamId (name, id)) = name + "-" + id

        let parse (x: string) = 
            let error () = Error "Invalid input argument."
            let parts = x.Split('-')
            if parts.Length <> 2 then error ()
            else 
                match Array.tryFind String.IsNullOrWhiteSpace parts with
                | None -> StreamId (parts.[0], parts.[1]) |> Ok
                | _ -> error ()

    type Aggregate<'state, 'command, 'event> = {
        Zero: 'state
        Execute: 'state -> 'command -> Result<'event list, string>
        Apply: 'state -> 'event -> Result<'state, string>
    }

    type StateBox<'state> = {
        StreamId: StreamId option
        Version: int
        State: 'state
    }

    type CommandBox<'command> = {
        StreamId: StreamId
        Command: 'command
    }

    type EventBox<'event> = {
        Version: int
        StreamId: StreamId
        Event: 'event
    }
