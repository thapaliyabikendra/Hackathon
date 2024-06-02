import { GroupFilter } from './../proxy/groups/models';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GroupService, GroupDto, CreateUpdateGroupDto } from '@proxy/groups';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss'],
  providers: [ListService],
})
export class GroupComponent implements OnInit  {
  data = { items: [], totalCount: 0 } as PagedResultDto<GroupDto>;
  form: FormGroup;
  selected = {} as GroupDto;
  isModalOpen = false;
  filter: GroupFilter = {};
  filterHideShow = false;

  constructor(public readonly list: ListService,
    private service: GroupService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toast: ToasterService) {}

  ngOnInit() {
    this.getData();
  }

  getData(){
    const streamCreator = (query) => this.service.getListByFilter(query, this.filter);
    this.list.hookToQuery(streamCreator).subscribe((response) => {
      this.data = response;
    });
  }

  toggleFilter(){
    this.filterHideShow = !this.filterHideShow;
  }

  create() {
    this.selected = {} as GroupDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  edit(id: string) {
    this.service.get(id).subscribe((data) => {
      this.selected = data;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      			tournamentId: [this.selected.tournamentId, Validators.required],
			groupName: [this.selected.groupName, Validators.required],

    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    const dto: CreateUpdateGroupDto = {
      			tournamentId: this.form.get("tournamentId").value,
			groupName: this.form.get("groupName").value,

    };
    const request = this.selected.id
      ? this.service.update(this.selected.id, dto)
      : this.service.create(dto);

    request.subscribe(() => {
      this.toast.success(this.selected.id?'::GroupUpdated':'::GroupCreated', "::SUCCESS", {
        tapToDismiss: true,
        life: 2500
      });
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation.warn('::PressOKToContinue', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.service.delete(id).subscribe(() => {
          this.toast.success('::GroupDeleted', "::SUCCESS", {
            tapToDismiss: true,
            life: 2500
          });
          this.list.get();
        });
      }
    });
  }
}
