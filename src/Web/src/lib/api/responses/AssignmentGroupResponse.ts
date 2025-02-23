import AssignmentResponse from "@/lib/api/responses/AssignmentResponse.ts";

type AssignmentGroupResponse = {
  id: number;
  name: string;
  description: string | null;
  weight: number;
  createdOnUtc: string;
  updatedOnUtc: string;
  assignments: AssignmentResponse[];
};

export default AssignmentGroupResponse;
