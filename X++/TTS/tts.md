# ttsBegin and ttsCommit

It's a transaction block for data changes between them.
They store in the memory all the changes that we are making to one or many records
ttsBegin: mark's the beginning of a transaction. This ensures data integrity and guarantees that all the updates performed until the transaction ends (by ttsCommit or ttsAbort) are consistent (all or none).

## TTS inside loops

If an error is thrown, only the last record will be reverted
