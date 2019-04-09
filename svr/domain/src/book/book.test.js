import { describe, it } from 'mocha';
import { expect } from 'chai';
import { zero, execute, apply } from './book';
import { createCmdOpenBook } from './commands';

const openBookData = { name: 'abc', openDate: new Date('2010-01-01') };

function createOpenBookEvent() {
  const cmd = createCmdOpenBook(openBookData).ok;
  return execute(zero(), cmd).ok;
}

function stateVerison1() {
  return zero().set('version', 1);
}

describe('Book tests', function() {
  it('should open book on zero state', function() {
    const cmd = createCmdOpenBook(openBookData).ok;
    const state = zero();
    const evn = execute(state, cmd).ok;
    expect(evn).to.include(openBookData);
  });

  it('should not open book on highier then zero state', function() {
    const cmd = createCmdOpenBook(openBookData).ok;
    const state = stateVerison1();
    const result = execute(state, cmd);
    expect(result.isError()).to.be.true;
  });

  it('should fail on unknown command', function() {
    const result = execute(zero(), {});
    expect(result.isError()).to.be.true;
  });

  it('should not apply event if version is incorrect', function() {
    const evn = createOpenBookEvent();
    const state = stateVerison1();
    const result = apply(state, evn);
    expect(result.isError()).to.be.true;
  });

  it('should not apply event if is unknown', function() {
    const result = apply(zero(), { version: 1 });
    expect(result.isError()).to.be.true;
    expect(result.toString()).to.contain('unknown event');
  });
});
