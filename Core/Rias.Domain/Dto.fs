namespace Rias.Domain

module DomainDto =
    open Rias.Contract.Domain
    open Data
    open System

    let asNumber x = Dto.Number (float x)
    let asString x = Dto.String x
    let asMap x = x |> Map.ofList |> Dto.Map
    let asDate (x: DateTime) = x.ToShortDateString() |> Dto.String
    let asStreamId x = x |> StreamId.toString |> Dto.String
    let asProperty (name, value) = Map.empty.Add(name, value) |> Dto.Map

module EventDto =
    open Rias.Contract.Domain
    open DomainDto

    let eventToDto dataToDto (event : EventBox<'event>) =
        [ ("streamId", event.StreamId |> asStreamId)
          ("version", event.Version |> asNumber)
          ("event", dataToDto event.Event)]
        |> asMap

module BookDto =
    open BookRoot
    open DomainDto

    let asOrdinalNumber x = x |> OrdinalNumber.value |> asNumber

    let bookOpenedToDto (data: Args.OpenNewBookArgs) =
        [ ("name", data.Name |> asString)
          ("date", data.Date |> asDate)]
        |> asMap

    let transactionAccountedToDto (data: Transaction) =
        [ ("ordinalNumber", data.OrdinalNumber |> asOrdinalNumber)
          ("accountingDate", data.AccountingDate |> asDate)
          ("transactionId", data.TransactionId |> asStreamId)]
        |> asMap

    let bookEventDataToDto (event : Event) =
        match event with
        | BookOpened data -> ("BookOpened", bookOpenedToDto data)
        | TransactionAccounted tdata -> ("TransactionAccounted", transactionAccountedToDto tdata)
        |> asProperty

    module api =
        let bookEventToDto = EventDto.eventToDto bookEventDataToDto
