namespace Rias.Domain

module StateBox =
    open StreamIdentity

    let zero zeroState = 
        { StreamId = None; Version = 0; State = zeroState }

    let isZero (state: StateBox<'state>) = state.Version = 0

    let validateZero (state: StateBox<'state>) =
        if isZero state then Ok state
        else Error "Zero state reuired"

    let nextStateBox oldBox state =
        { oldBox with Version = oldBox.Version + 1; State = state }

    let private validateSequence (state: StateBox<'state>) (event: EventBox<'event>) =
        if state.Version + 1 = event.Version then Ok event
        else Error "Invalid sequence numeber"

    let apply fapply (state: StateBox<'state>) (event: EventBox<'event>) =
        event |> validateSequence state 
                |> Result.bind (fapply state.State)
                |> Result.map (nextStateBox state)
