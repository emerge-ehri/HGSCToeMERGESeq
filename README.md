# HGSC To eMERGESeq
Proof of concept conversion to the eMERGESeq XML format from the HGSC JSON format

## Limitations
Currently this has a static lookup for codes.  If a new code is encountered that is not in a lookup collection, the program will throw an exception.  At that point the code will need to be added to the appropriate collection and the program re-run.
