import { UppercaseFirstLetters } from "@/lib/utils.ts";

export function IsRequired(property: string) {
  return `${UppercaseFirstLetters(property)} property is required.`;
}

export function IsInvalid(property: string) {
  return `${UppercaseFirstLetters(property)} property is invalid.`;
}

export function IsBetween(property: string, min: number, max: number) {
  return `${property} property must have between ${min} and ${max} characters.`;
}

export const IsInvalidPassword =
  "Password property must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.";
