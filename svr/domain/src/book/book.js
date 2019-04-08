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

export function apply(state, event) {
  state;
  event;
}

function validateIsZero(state) {
  return state.version === 0 ? ok(state) : error('state is not zero');
}

export function execute(state, command) {
  const name = Record.getDescriptiveName(command);
  switch (name) {
  case 'CmdOpenBook':
    return validateIsZero(state).map(() => createEvnBookOpened(command));
  default:
    return error('unknown command');
  }
}
