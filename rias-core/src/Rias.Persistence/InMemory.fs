namespace Rias.Persistence

module InMemory =
    open Rias.Common
    
    let private getStreamId (dto: Dto) =
        match dto with
        | Map map -> match Map.find "streamId" map with
                     | String v -> v
                     | _ -> ""
        | _ -> ""

    let private getVersion (dto: Dto) =
        match dto with
        | Map map -> match Map.find "version" map with
                     | Number n -> n
                     | _ -> 0.0
        | _ -> 0.0

    let create () =
        let collection = ResizeArray<Dto>()
        let store events = events |> Seq.iter collection.Add |> Ok
        let load streamId = collection |> Seq.filter (fun e -> getStreamId e = streamId)
                                       |> Seq.sortBy getVersion
                                       |> Ok

        { store = fun x -> async { return store x }
          load = fun x -> async { return load x } }
