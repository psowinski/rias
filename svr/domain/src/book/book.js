import { Record } from 'immutable';
import { error, ok } from 'riasfp';
import { createEvnBookOpened } from './events';

const Book = new Record(
  {
    version: 0,
    name: '',
    openDate: undefined
  },
  'Book'
);

export function zero() {
  return new Book();
}

function validateStateVersion(state, expectedVersion) {
  return state.version + 1 === expectedVersion
    ? ok(state)
    : error('incorrect state version');
}

function updateStateVersion(state) {
  return state.set('version', state.version + 1);
}

function applyEvent(state, event) {
  const name = Record.getDescriptiveName(event);
  switch (name) {
  case 'EvnBookOpened':
    return ok();
  default:
    return error('unknown event');
  }
}

export function apply(state, event) {
  return validateStateVersion(state, event.version)
    .bind(s => applyEvent(s, event))
    .map(updateStateVersion);
}

function validateIsZero(state) {
  return state.version === 0 ? ok(state) : error('state is not zero');
}

export function execute(state, command) {
  const name = Record.getDescriptiveName(command);
  switch (name) {
  case 'CmdOpenBook':
    return validateIsZero(state).map(s => createEvnBookOpened(s, command));
  default:
    return error('unknown command');
  }
}
