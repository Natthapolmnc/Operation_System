from typing import Tuple
import os
import hashlib
import hmac

def hash_new_password(password: str) -> Tuple[bytes, bytes]:
    salt = os.urandom(16)
    pw_hash = hashlib.pbkdf2_hmac('sha256', password.encode(), salt, 100000)
    return salt, pw_hash

def is_correct_password(salt: bytes, pw_hash: bytes, password: str) -> bool:
    return hmac.compare_digest(
        pw_hash,
        hashlib.pbkdf2_hmac('sha256', password.encode(), salt, 100000)
    )
    
print ("\n")
passwrd=input("password :")
salt,pw_hash=hash_new_password(passwrd)
print ("your salt is: "+str(salt))
print ("your hash pwd is: "+str(pw_hash)+"\n")
print ("checking....")
print (is_correct_password(salt,pw_hash,passwrd))
