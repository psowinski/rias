import { Record } from 'immutable';
import { ok, error } from 'riasfp';
import { isNonEmptyString, isValidDate } from '../util';

const CmdOpenBook = new Record(
  {
    name: '',
    openDate: new Date('2000-01-01')
  },
  'CmdOpenBook'
);

function validateName(data) {
  return isNonEmptyString(data.name) ? ok(data) : error('invalid name');
}

function validateOpenDate(data) {
  return isValidDate(data.openDate) ? ok(data) : error('invalid date');
}

export function createCmdOpenBook(data) {
  return validateName(data)
    .bind(validateOpenDate)
    .map(x => new CmdOpenBook(x));
}
