import { describe, it } from 'mocha';
import { expect } from 'chai';
import { zero, execute } from './book';
import { createCmdOpenBook } from './commands';

const openBookData = { name: 'abc', openDate: new Date('2010-01-01') };

describe('Book tests', function() {
  it('should open book on zero state', function() {
    const cmd = createCmdOpenBook(openBookData).ok;
    const state = zero();
    const evn = execute(state, cmd).ok;
    expect(evn).to.include(openBookData);
  });

  it('should not open book on highier then zero state', function() {
    const cmd = createCmdOpenBook(openBookData).ok;
    const state = zero().set('version', 1);
    const result = execute(state, cmd);
    expect(result.isError()).to.be.true;
  });

  it('should fail on unknown command', function() {
    const result = execute(zero(), {});
    expect(result.isError()).to.be.true;
  });
});
