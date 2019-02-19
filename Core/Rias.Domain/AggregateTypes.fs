namespace Rias.Domain

[<AutoOpen>]
module AggregateTypes =

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
        StreamId: StreamId
        Version: int
        Event: 'event
    }
